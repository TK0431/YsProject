using System;
using System.Windows.Controls;
using YsProject.Consts;
using YsProject.Utility;

namespace YsProject.Pages
{
    public class BasePage : Page
    {
        protected void PageChange(PageEnum page)
            => NavigationService.Navigate(new Uri("Pages/UI" + page.GetValue() + ".xaml", UriKind.RelativeOrAbsolute));
    }
}
