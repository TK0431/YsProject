using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using YsProject.Consts;
using YsProject.Utils;
using YsProject.ViewModels;

namespace YsProject.Logics
{
    public class UI902Logic : BaseLogic
    {
        private Dictionary<string, string> _dic = null;

        /// <summary>
        /// 字典选择
        /// </summary>
        /// <param name="model"></param>
        public void InputDictionary(UI902ViewModel model)
        {
            string file = FileDialog();

            if (file == null) return;

            model.FileDictionary = file;

            InsertSheetData(model);
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="model"></param>
        public void GetOut(UI902ViewModel model)
        {
            model.Out = string.Empty;

            if (string.IsNullOrWhiteSpace(model.In)) return;

            if (!string.IsNullOrWhiteSpace(model.FileDictionary) && _dic == null) InsertSheetData(model);

            if (model.Item.Index == (int)EnumDevLang.CSHUP)
            {

                foreach (string item in model.In.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
                {
                    if (string.IsNullOrWhiteSpace(item)) continue;

                    if (model.ReverseFlg)
                        AddReverseText(model, item);
                    else
                        AddText(model, item);
                }
            }
            else
            {
                MessageBox.Show("C#以外，未实装");
            }
        }

        /// <summary>
        /// Sheet Data Add
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="sheet"></param>
        private void InsertSheetData(UI902ViewModel model)
        {
            _dic = new Dictionary<string, string>();
            using (ExcelUtility excel = new ExcelUtility(model.FileDictionary))
            {
                ExcelWorksheet sheet = excel.GetSheet();

                for (int i = 2, j = 1; ; i++)
                {
                    if (i == 2 && string.IsNullOrWhiteSpace(sheet.Cells[i, j].Text)) break;

                    if (string.IsNullOrWhiteSpace(sheet.Cells[i, j].Text))
                    {
                        i = 1;
                        j += 3;
                        continue;
                    }

                    try
                    {
                        _dic.Add(sheet.Cells[i, j].Text, sheet.Cells[i, j + 1].Text);
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="itme"></param>
        private void AddText(UI902ViewModel model, string item)
        {
            StringBuilder txt = new StringBuilder();
            txt.AppendLine("        /// <summary>");
            txt.AppendLine($"        /// {item}");
            txt.AppendLine("        /// <summary>");
            txt.AppendLine($"        public string {GetDicTxt(item)}" + " { get; set; }");
            txt.AppendLine("");

            model.Out += txt.ToString();
        }

        private string GetDicTxt(string item)
        {
            if (_dic.ContainsKey(item))
            {
                return _dic[item];
            }
            for (int i = item.Length - 1; i > 0; i--)
            {
                if (_dic.ContainsKey(item.Substring(0, i)))
                    return GetDicTxt(item.Substring(0, i)) + GetDicTxt(item.Substring(i, item.Length - 1));
            }

            return item;
        }

        private void AddReverseText(UI902ViewModel model, string item)
        {
            StringBuilder txt = new StringBuilder();
            txt.AppendLine("        /// <summary>");
            txt.AppendLine($"        /// {GetReverseDicTxt(item)}");
            txt.AppendLine("        /// <summary>");
            txt.AppendLine($"        public string {item}" + " { get; set; }");
            txt.AppendLine("");

            model.Out += txt.ToString();
        }

        private string GetReverseDicTxt(string item)
        {
            foreach (string key in _dic.Keys)
            {
                if (_dic[key] == item) return _dic[key];
            }

            return item;
        }
    }
}
