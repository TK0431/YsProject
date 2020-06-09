using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Security.Cryptography;
using System.Text;
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
            Dictionary<string, string> dic = ComUtility.GetRecordValue(model.Page.ToString());
            string project = dic["project"];
            if (string.IsNullOrWhiteSpace(project)) return;

            // 默认选择上次选择项目
            foreach (TB_Project item in model.ProjectItems)
            {
                if (item.CD == project)
                {
                    model.SelectedProjectItem = item;
                    return;
                }
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

                ExcelWorksheet sheet = excel.GetSheet("Project");
                if (sheet == null) return;

                // Project
                TB_Project pro = new TB_Project()
                {
                    CD = sheet.Cells[2, 1].Text,
                    Name = sheet.Cells[2, 2].Text,
                };

                // 项目ID 项目名 Check
                if (string.IsNullOrWhiteSpace(pro.CD) || string.IsNullOrWhiteSpace(pro.Name))
                {
                    return;
                }

                // 项目期间  ****************************************change
                DateTime temp1;
                if (DateTime.TryParse(sheet.Cells[2, 3].Text, out temp1))
                    pro.DateStart = temp1;
                DateTime temp2;
                if (DateTime.TryParse(sheet.Cells[2, 4].Text, out temp2))
                    pro.DateEnd = temp2;

                pro.DelFlg = false;
                pro.InserterCd = "139047";
                pro.InserteTime = db.DbConectTime;
                pro.UpdaterCd = "139047";
                pro.UpdateTime = db.DbConectTime;

                // 删除既存
                db.DeleteAll("delete from TB_project");
                db.Add(pro);

                #endregion

                #region User

                List<TB_User> userList = new List<TB_User>();
                sheet = excel.GetSheet("User");
                for (int i = 2; i <= excel.GetMaxRow(sheet, 1); i++)
                {
                    // 密码
                    string md5 = ComUtility.GetMD5(string.IsNullOrWhiteSpace(sheet.Cells[i, 3].Text) ? "123" : sheet.Cells[i, 3].Text);

                    // 权限
                    int level;
                    if (int.TryParse(sheet.Cells[i, 5].Text, out level))
                    {
                        if (level < 0 || level > 4) level = 0;
                    }
                    else level = 0;

                    TB_User user = new TB_User()
                    {
                        ProjectCD = pro.CD,
                        CD = sheet.Cells[i, 1].Text,
                        Name = sheet.Cells[i, 2].Text,
                        Password = md5,
                        IP = sheet.Cells[i, 4].Text,
                        Level = level,
                    };

                    // 项目期间  ****************************************change
                    DateTime temp3;
                    if (DateTime.TryParse(sheet.Cells[i, 6].Text, out temp3))
                        user.DateStart = temp3;
                    DateTime temp4;
                    if (DateTime.TryParse(sheet.Cells[i, 7].Text, out temp4))
                        user.DateEnd = temp4;

                    user.DelFlg = false;
                    user.InserterCd = "139047";
                    user.InserteTime = db.DbConectTime;
                    user.UpdaterCd = "139047";
                    user.UpdateTime = db.DbConectTime;

                    userList.Add(user);
                }

                db.DeleteAll("delete from TB_user");
                db.Add(userList);

                #endregion

                #region Group

                List<TB_Group> groupList = new List<TB_Group>();
                sheet = excel.GetSheet("Group");
                for (int i = 2; i <= excel.GetMaxRow(sheet, 1); i++)
                {
                    TB_Group group = new TB_Group()
                    {
                        ProjectCD = pro.CD,
                        CD = sheet.Cells[i, 1].Text,
                        Name = sheet.Cells[i, 2].Text,
                        UserCD = sheet.Cells[i, 3].Text,
                    };

                    // 项目期间  ****************************************change
                    DateTime temp5;
                    if (DateTime.TryParse(sheet.Cells[i, 4].Text, out temp5))
                        group.DateFrom = temp5;
                    DateTime temp6;
                    if (DateTime.TryParse(sheet.Cells[i, 5].Text, out temp6))
                        group.DateEnd = temp6;

                    group.DelFlg = false;
                    group.InserterCd = "139047";
                    group.InserteTime = db.DbConectTime;
                    group.UpdaterCd = "139047";
                    group.UpdateTime = db.DbConectTime;

                    groupList.Add(group);
                }
                db.DeleteAll("delete from tb_group");
                db.Add(groupList);

                #endregion

                #region Function

                List<TB_Function> funcList = new List<TB_Function>();
                sheet = excel.GetSheet("Function");
                for (int i = 2; i <= excel.GetMaxRow(sheet, 1); i++)
                {
                    TB_Function func = new TB_Function()
                    {
                        ProjectCD = pro.CD,
                        Group = sheet.Cells[i, 1].Text,
                        CD = sheet.Cells[i, 2].Text,
                        Name = sheet.Cells[i, 3].Text,
                        Type = sheet.Cells[i, 4].Text,
                    };

                    // 项目期间  ****************************************change
                    DateTime temp7;
                    if (DateTime.TryParse(sheet.Cells[i, 5].Text, out temp7))
                        func.DateEnd = temp7;

                    func.DelFlg = false;
                    func.InserterCd = "139047";
                    func.InserteTime = db.DbConectTime;
                    func.UpdaterCd = "139047";
                    func.UpdateTime = db.DbConectTime;

                    funcList.Add(func);
                }
                db.DeleteAll("delete from tb_function");
                db.Add(funcList);

                #endregion
            }
        }
    }
}
