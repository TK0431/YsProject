using System;
using System.Collections.Generic;
using YsProject.Models;
using YsProject.Utility;
using YsProject.ViewModels;

namespace YsProject.Pages
{
    /// <summary>
    /// 字典转Model
    /// </summary>
    public partial class UI902 : BasePage
    {
        /// <summary>
        /// Model
        /// </summary>
        private UI902ViewModel _model;

        public UI902()
        {
            InitializeComponent();

            _model = new UI902ViewModel();
            this.DataContext = _model;

            Dispatcher.ShutdownStarted += OnDispatcherShutdownStarted;
        }

        private void BasePage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Dictionary<string, string> dic = ComUtility.GetXmValues(_model.Page.ToString());

            _model.FileDictionary = dic["file"];
            _model.ReverseFlg = dic["reverse"].ToUpper() == "TRUE" ? true : false;
            foreach (EnumItem ei in _model.Items)
            {
                if (ei.Description == dic["language"])
                    _model.Item = ei;
            }
        }

        private void OnDispatcherShutdownStarted(object sender, EventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("file", _model.FileDictionary);
            dic.Add("reverse", _model.ReverseFlg.ToString());
            dic.Add("language", _model.Item.Description);
            ComUtility.SetRecordValue(_model.Page.ToString(), dic);
        }
    }
}
