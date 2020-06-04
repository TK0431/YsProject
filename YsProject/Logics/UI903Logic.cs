using System;
using System.Text;
using YsProject.ViewModels;

namespace YsProject.Logics
{
    public class UI903Logic : BaseLogic
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="model"></param>
        public void GetOut(UI903ViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.In)) return;

            if (string.IsNullOrWhiteSpace(model.LeftTxt) && string.IsNullOrWhiteSpace(model.RightTxt)) return;

            model.Out = string.Empty;
            StringBuilder txt = new StringBuilder();
            foreach (string item in model.In.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
            {
                txt.AppendLine(model.LeftTxt + item + model.RightTxt);
            }
            model.Out = txt.ToString();
        }
    }
}
