using YsProject.ViewModels;

namespace YsProject.Pages
{
    /// <summary>
    /// Wbs.xaml の相互作用ロジック
    /// </summary>
    public partial class UI001 : BasePage
    {
        private UI001ViewModel _model;

        public UI001()
        {
            InitializeComponent();

            _model = new UI001ViewModel();
            this.DataContext = _model;
        }
    }
}
