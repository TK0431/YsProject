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
    public class WD001Logic : BaseLogic
    {
        public void Login(WD001ViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.UserId) || string.IsNullOrWhiteSpace(model.PassWord)) return;

            using (EntityDao db = new EntityDao())
            {
                List<MySqlParameter> para = new List<MySqlParameter>();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select * from tb_user");
                para.Add(new MySqlParameter("CD", model.UserId));
                sql.AppendLine("where CD = @CD");
                para.Add(new MySqlParameter("Password", model.PassWord));
                sql.AppendLine("  and Password = md5(@Password)");
                sql.AppendLine("  and DateStart <= CURDATE()");
                sql.AppendLine("  and CURDATE()  <= DateEnd");
                sql.AppendLine("  and DelFlg = '0'");

                TB_User user = db.FindSingle<TB_User>(sql.ToString(), para);

                if (user != null)
                {
                    App.LoginUser = user;
                    model.UserName = user.Name;
                }
                else
                { 
                }
            }
        }
    }
}
