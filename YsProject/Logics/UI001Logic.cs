using MySql.Data.MySqlClient;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using YsProject.Consts;
using YsProject.Models.DB;
using YsProject.Utility;
using YsProject.Utils;
using YsProject.ViewModels;

namespace YsProject.Logics
{
    public class UI001Logic : BaseLogic
    {
        /// <summary>
        /// 项目下拉框获取数据
        /// </summary>
        /// <param name="model"></param>
        public void GetProjects(UI001ViewModel model)
        {
            // 从数据库获取数据
            using (EntityDao db = new EntityDao())
            {
                model.ProjectItems = new ObservableCollection<TB_Project>(db.FindAll<TB_Project>());
            }

            // 从Xml文件中获取上次选择项目
            Dictionary<string, string> dic = ComUtility.GetXmValues(model.Page.ToString());
            string project = dic["project"];
            if (string.IsNullOrWhiteSpace(project)) return;

            // 默认选择上次选择项目
            foreach (TB_Project item in model.ProjectItems)
            {
                if (item.CD == project)
                {
                    model.SelectedProjectItem = item;
                    GetDataItems(model);
                    return;
                }
            }
        }

        public void GetDataItems(UI001ViewModel model)
        {
            using (EntityDao db = new EntityDao())
            {
                StringBuilder sql = new StringBuilder();
                List<MySqlParameter> param = new List<MySqlParameter>();
                sql.AppendLine("SELECT ");
                sql.AppendLine("  @rownum := @rownum +1 AS rownum,");
                sql.AppendLine("  B.Group,");
                sql.AppendLine("  B.CD,");
                sql.AppendLine("  B.Name,");
                sql.AppendLine("  B.Type,");
                sql.AppendLine("  B.DateEnd,");
                sql.AppendLine("  D.Name AS Work,");
                sql.AppendLine("  C.UserCD,");
                sql.AppendLine("  C.DatePeFrom,");
                sql.AppendLine("  C.DatePeEnd,");
                sql.AppendLine("  C.DateReFrom,");
                sql.AppendLine("  C.DateReEnd,");
                sql.AppendLine("  C.Percent,");
                sql.AppendLine("  C.Back");
                sql.AppendLine("FROM (SELECT @rownum := 0) r,");
                sql.AppendLine("TB_project AS A");
                sql.AppendLine("INNER JOIN tb_function AS B");
                sql.AppendLine("ON A.CD = B.ProjectCD");
                sql.AppendLine("INNER JOIN tb_wbstype AS C");
                sql.AppendLine("ON A.CD = C.ProjectCD");
                sql.AppendLine("AND B.CD = C.CD");
                sql.AppendLine("LEFT JOIN tb_type AS D");
                sql.AppendLine("ON D.Type = '04'");
                sql.AppendLine("AND C.Type = D.Value");
                param.Add(new MySqlParameter("CD", model.SelectedProjectItem.CD));
                sql.AppendLine("WHERE A.CD = @CD");
                sql.AppendLine("ORDER BY");
                sql.AppendLine("B.Group,B.CD,C.Type");

                model.DataItems = new ObservableCollection<UI001DataItem>(db.FindAll<UI001DataItem>(sql.ToString(), param));
            }
        }

        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <param name="model"></param>
        public void UploadExcel(UI001ViewModel model)
        {
            // 对话框选择文件
            string file = FileDialog();
            if (file == null) return;

            // 读取Excel
            using (EntityDao db = new EntityDao(true))
            using (ExcelUtility excel = new ExcelUtility(file))
            {
                #region Project

                // 获取Project Sheet
                ExcelWorksheet sheet = excel.GetSheet("Project");
                if (sheet == null) return;

                // 数据设定
                TB_Project pro = new TB_Project()
                {
                    CD = sheet.Cells[2, 1].Text,
                    Name = sheet.Cells[2, 2].Text,
                    DateStart = DataUtility.CDateDB(sheet.Cells[2, 3].Text),
                    DateEnd = DataUtility.CDateDB(sheet.Cells[2, 4].Text),
                };

                // 项目ID 项目名 Check
                if (string.IsNullOrWhiteSpace(pro.CD) || string.IsNullOrWhiteSpace(pro.Name))
                {
                    return;
                }

                // 删除既存
                int k = db.DeleteAll("delete from TB_project where CD = @CD", new List<MySqlParameter>() { new MySqlParameter("CD", pro.CD) });
                // 添加数据
                db.AddData(pro);

                #endregion

                #region User

                // 获取User Sheet
                sheet = excel.GetSheet("User");

                // 数据设定
                List<TB_User> userList = new List<TB_User>();
                for (int i = 2; i <= excel.GetMaxRow(sheet, 1); i++)
                {
                    // 密码(默认123)转MD5
                    string md5 = ComUtility.GetMD5(string.IsNullOrWhiteSpace(sheet.Cells[i, 3].Text) ? "123" : sheet.Cells[i, 3].Text);

                    // 权限
                    int level = DataUtility.CIntDB(sheet.Cells[i, 5].Text, 1);
                    if (level <= 0 || level > 4) level = 1;

                    // 数据设定
                    TB_User user = new TB_User()
                    {
                        ProjectCD = pro.CD,
                        CD = sheet.Cells[i, 1].Text,
                        Name = sheet.Cells[i, 2].Text,
                        Password = md5,
                        IP = DataUtility.CIPDB(sheet.Cells[i, 4].Text),
                        Level = level,
                        DateStart = DataUtility.CDateDB(sheet.Cells[i, 6].Text),
                        DateEnd = DataUtility.CDateDB(sheet.Cells[i, 7].Text),
                    };

                    // 员工号 用户名 Check
                    if (string.IsNullOrWhiteSpace(user.CD) || string.IsNullOrWhiteSpace(user.Name)) continue;

                    userList.Add(user);
                }

                // 删除既存
                db.DeleteAll("delete from TB_user where ProjectCD = @ProjectCD", new List<MySqlParameter>() { new MySqlParameter("ProjectCD", pro.CD) });
                // 添加数据
                db.AddData(userList);

                #endregion

                #region Group

                // 获取Group Sheet
                sheet = excel.GetSheet("Group");

                // 数据设定
                List<TB_Group> groupList = new List<TB_Group>();
                for (int i = 2; i <= excel.GetMaxRow(sheet, 1); i++)
                {
                    // 数据设定
                    TB_Group group = new TB_Group()
                    {
                        ProjectCD = pro.CD,
                        CD = sheet.Cells[i, 1].Text,
                        Name = sheet.Cells[i, 2].Text,
                        UserCD = sheet.Cells[i, 3].Text,
                        DateFrom = DataUtility.CDateDB(sheet.Cells[i, 4].Text),
                        DateEnd = DataUtility.CDateDB(sheet.Cells[i, 5].Text),
                    };

                    // 组号 用户名 Check
                    if (string.IsNullOrWhiteSpace(group.CD) || string.IsNullOrWhiteSpace(group.Name)) continue;

                    groupList.Add(group);
                }

                // 删除既存
                db.DeleteAll("delete from tb_group where ProjectCD = @ProjectCD", new List<MySqlParameter>() { new MySqlParameter("ProjectCD", pro.CD) });
                // 添加数据
                db.AddData(groupList);

                #endregion

                #region Function

                // 获取Function Sheet
                sheet = excel.GetSheet("Function");

                // 数据设定
                List<TB_Function> funcList = new List<TB_Function>();
                List<TB_WbsType> wbsList = new List<TB_WbsType>();
                for (int i = 2; i <= excel.GetMaxRow(sheet, 2); i++)
                {
                    // 数据设定
                    TB_Function func = new TB_Function()
                    {
                        ProjectCD = pro.CD,
                        Group = sheet.Cells[i, 1].Text,
                        CD = sheet.Cells[i, 2].Text,
                        Name = sheet.Cells[i, 3].Text,
                        Type = sheet.Cells[i, 4].Text,
                        DateEnd = DataUtility.CDateDB(sheet.Cells[i, 5].Text),
                    };

                    // 机能ID Check
                    if (string.IsNullOrWhiteSpace(func.CD)) continue;

                    // 作业种类数据添加
                    typeof(EnumDevType).GetList(true).ForEach(x =>
                        wbsList.Add(new TB_WbsType()
                        {
                            ProjectCD = pro.CD,
                            CD = func.CD,
                            Type = x.Value,
                        })
                    );

                    funcList.Add(func);
                }

                // 删除既存
                db.DeleteAll("delete from tb_function where ProjectCD = @ProjectCD", new List<MySqlParameter>() { new MySqlParameter("ProjectCD", pro.CD) });
                db.DeleteAll("delete from tb_wbstype where ProjectCD = @ProjectCD", new List<MySqlParameter>() { new MySqlParameter("ProjectCD", pro.CD) });
                // 添加数据
                db.AddData(funcList);
                db.AddData(wbsList);

                #endregion
            }
        }
    }
}
