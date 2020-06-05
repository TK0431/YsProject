using YsTool.ViewModels;

namespace YsProject.Pages
{
    /// <summary>
    /// AppAnalyse.xaml の相互作用ロジック
    /// </summary>
    public partial class UI202 : BasePage
    {
        private UI202ViewModel _model;

        public UI202()
        {
            InitializeComponent();

            _model = new UI202ViewModel();
            this.DataContext = _model;
        }
    }
}
