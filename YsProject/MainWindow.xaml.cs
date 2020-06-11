using System.Windows;
using System.Windows.Controls;
using YsProject.Consts;
using YsProject.Models;
using YsProject.Pages;
using YsProject.ViewModels;

namespace YsProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new MainWindowViewModel();
            this.DataContext = _viewModel;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //UpdateEnum.Update();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuList_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            EnumItem selected = ((ListBox)sender).SelectedItem as EnumItem;

            if (selected != null)
            {
                switch ((PageEnum)selected.Index)
                {
                    case PageEnum.UI000:
                        _viewModel.MainPage = new UI000();
                        break;
                    case PageEnum.UI001:
                        _viewModel.MainPage = new UI001();
                        break;
                    case PageEnum.UI002:
                        _viewModel.MainPage = new UI002();
                        break;
                    case PageEnum.UI003:
                        _viewModel.MainPage = new UI003();
                        break;
                    case PageEnum.UI101:
                        _viewModel.MainPage = new UI101();
                        break;
                    case PageEnum.UI102:
                        _viewModel.MainPage = new UI102();
                        break;
                    case PageEnum.UI201:
                        _viewModel.MainPage = new UI201();
                        break;
                    case PageEnum.UI202:
                        _viewModel.MainPage = new UI202();
                        break;
                    case PageEnum.UI902:
                        _viewModel.MainPage = new UI902();
                        break;
                    case PageEnum.UI903:
                        _viewModel.MainPage = new UI903();
                        break;
                    default:
                        break;
                }
            }

            expWbs.IsExpanded = false;
            expApp.IsExpanded = false;
            expWeb.IsExpanded = false;
            MenuToggleButton.IsChecked = false;
        }
    }
}
