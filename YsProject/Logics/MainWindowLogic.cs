using MaterialDesignThemes.Wpf;
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
using YsProject.Windows;

namespace YsProject.Logics
{
    public class MainWindowLogic : BaseLogic
    {
        public void Init(MainWindowViewModel model)
        {
            using (EntityDao db = new EntityDao())
            {
                List<MySqlParameter> para = new List<MySqlParameter>();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select * from tb_user");
                para.Add(new MySqlParameter("IP", ComUtility.GetLocalIPV4().ToString()));
                sql.AppendLine("where IP = @IP");
                sql.AppendLine("  and DateStart <= CURDATE()");
                sql.AppendLine("  and CURDATE()  <= DateEnd");
                sql.AppendLine("  and DelFlg = '0'");
                TB_User user = db.FindSingle<TB_User>(sql.ToString(), para);

                if (user != null)
                {
                    App.LoginUser = user;
                }
            }
        }
    }
}
