using YsProject.ViewModels;

namespace YsProject.Pages
{
    /// <summary>
    /// 字典转Model
    /// </summary>
    public partial class UI903 : BasePage
    {
        /// <summary>
        /// Model
        /// </summary>
        private UI903ViewModel _model;

        public UI903()
        {
            InitializeComponent();

            _model = new UI903ViewModel();
            this.DataContext = _model;
        }
    }
}
