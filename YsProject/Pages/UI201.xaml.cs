using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using YsProject.Consts;
using YsProject.ViewModels;

namespace YsProject.Pages
{
    /// <summary>
    /// 浏览器模拟
    /// </summary>
    public partial class UI201 : BasePage
    {
        /// <summary>
        /// Model
        /// </summary>
        private UI201ViewModel _model;

        /// <summary>
        /// 构造
        /// </summary>
        public UI201()
        {
            InitializeComponent();

            // Model绑定
            _model = new UI201ViewModel();
            this.DataContext = _model;
        }

        /// <summary>
        /// Analyse按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnalyse_Click(object sender, RoutedEventArgs e)
        {
            // 页面加载
            this.webBrowser.Navigate(new Uri(_model.Url));
        }

        /// <summary>
        /// webBrowser加载页面函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webBrowser_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            SetWebBrowserSilent(sender as WebBrowser, true);
        }

        /// <summary>
        /// 设置浏览器静默，不弹错误提示框
        /// </summary>
        /// <param name="webBrowser">要设置的WebBrowser控件浏览器</param>
        /// <param name="silent">是否静默</param>
        private void SetWebBrowserSilent(WebBrowser webBrowser, bool silent)
        {
            FieldInfo fi = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fi != null)
            {
                object browser = fi.GetValue(webBrowser);
                if (browser != null)
                    browser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, browser, new object[] { silent });
            }
        }

        /// <summary>
        /// 页面转移到List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGoList_Click(object sender, RoutedEventArgs e)
        {
            this.PageChange(PageEnum.UI202);
        }
    }
}
