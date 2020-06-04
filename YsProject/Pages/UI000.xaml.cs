using System.Windows;
using YsProject.Consts;

namespace YsProject.Pages
{
    /// <summary>
    /// Main.xaml の相互作用ロジック
    /// </summary>
    public partial class UI000 : BasePage
    {
        public UI000()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.PageChange(PageEnum.UI002);
        }
    }
}
