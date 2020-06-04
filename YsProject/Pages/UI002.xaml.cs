using System.Windows.Controls;
using YsProject.ViewModels;

namespace YsProject.Pages
{
    /// <summary>
    /// User.xaml の相互作用ロジック
    /// </summary>
    public partial class UI002 : BasePage
    {
        private UI002ViewModel _model;

        public UI002()
        {
            InitializeComponent();

            _model = new UI002ViewModel();
            this.DataContext = _model;
        }

        private void Items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UI002Item item = this.Items.SelectedItem as UI002Item;
            if (item == null) return;

            _model.Cd = item.Cd;
            _model.Name = item.Name;
            string[] arr = item.IP.Split('.');
            _model.IP1 = int.Parse( arr[0]);
            _model.IP2 = int.Parse(arr[1]);
            _model.IP3 = int.Parse(arr[2]);
            _model.IP4 = int.Parse(arr[3]);
            _model.GroupId = item.Group;
        }
    }
}
