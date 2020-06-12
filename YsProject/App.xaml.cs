using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using YsProject.Consts;
using YsProject.Models.DB;
using YsProject.Utility;

namespace YsProject
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 语言
        /// </summary>
        public static EnumLanguage Language { get; set; } = EnumLanguage.CN;

        /// <summary>
        /// 语言
        /// </summary>
        public static TB_User LoginUser { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            // 语言获取
            switch (Enum.Parse(typeof(EnumLanguage),ComUtility.GetXmValue("language")))
            {
                case EnumLanguage.EN:
                    Language = EnumLanguage.EN;
                    break;
                case EnumLanguage.JP:
                    Language = EnumLanguage.JP;
                    break;
                default:
                    Language = EnumLanguage.CN;
                    break;
            }

            // 更换语言包
            UpdateLanguage();
        }

        /// <summary>
        /// 更换语言包
        /// </summary>
        public static void UpdateLanguage()
        {
            // 获取全部资源
            List<ResourceDictionary> dictionaryList = new List<ResourceDictionary>();
            foreach (ResourceDictionary dictionary in Application.Current.Resources.MergedDictionaries)
            {
                dictionaryList.Add(dictionary);
            }

            // 需要语言资源
            string requestedLanguage = $@"Styles/Language{Language.GetValue()}.xaml";
            ResourceDictionary resourceDictionary = dictionaryList.FirstOrDefault(d => d.Source.OriginalString.Equals(requestedLanguage));

            if (resourceDictionary == null)
            {
                ResourceDictionary resourceCN = dictionaryList.FirstOrDefault(d => d.Source.OriginalString.Equals(@"Styles/LanguageCN.xaml"));
                if(resourceCN != null) Application.Current.Resources.MergedDictionaries.Remove(resourceCN);
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            }
        }
    }
}
