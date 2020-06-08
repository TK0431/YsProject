using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using YsProject.Models.DB;
using YsProject.Utility;
using YsProject.Utils;
using YsProject.ViewModels;

namespace YsProject.Logics
{
    public class UI001Logic : BaseLogic
    {
        public void GetProjects(UI001ViewModel model)
        {
            using (EntityDao db = new EntityDao())
            {
                model.ProjectItems = new ObservableCollection<TB_Project>(db.FindAll<TB_Project>());
            }

            Dictionary<string, string> dic = ComUtility.GetRecordValue(model.Page.ToString());

            string project = dic["project"];
            if (string.IsNullOrWhiteSpace(project)) return;

            foreach (TB_Project item in model.ProjectItems)
            {
                if (item.CD == project)
                {
                    model.SelectedProjectItem = item;
                    return;
                }
            }
        }

        public void UploadExcel(UI001ViewModel model)
        {
            string file = FileDialog();

            if (file == null) return;

            using (ExcelUtility excel = new ExcelUtility())
            {
                ExcelWorksheet sheet = excel.GetSheet("Project");

                // Project
                TB_Project pro = new TB_Project()
                {
                    CD = sheet.Cells[2, 1].Text,
                    Name = sheet.Cells[2, 2].Text,

                };

                DateTime temp;
                if (DateTime.TryParse(sheet.Cells[2, 3].Text, out temp))
                    pro.DateStart = temp;
                else 
                {
                }
            }
        }
    }
}
