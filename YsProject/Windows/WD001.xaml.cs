using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using YsProject.ViewModels;

namespace YsProject.Windows
{
    /// <summary>
    /// WD001.xaml の相互作用ロジック
    /// </summary>
    public partial class WD001 : Window
    {
        /// <summary>
        /// Model
        /// </summary>
        private WD001ViewModel _model;

        public WD001()
        {
            InitializeComponent();

            _model = new WD001ViewModel();
            DataContext = _model;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
