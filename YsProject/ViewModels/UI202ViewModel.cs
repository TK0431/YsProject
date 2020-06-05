using MySqlX.XDevAPI.Common;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using YsProject.Logics;
using YsTool.ViewModels.Base;

namespace YsTool.ViewModels
{
    /// <summary>
    /// 浏览器模拟
    /// </summary>
    public class UI202ViewModel : BaseViewModel
    {
        /// <summary>
        /// Logic
        /// </summary>
        private UI202Logic _logic = new UI202Logic();

        /// <summary>
        /// 检索按钮
        /// </summary>
        public ICommand BtnUpLoad { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public ObservableCollection<UI202Item> ResultList { get; set; }

        public UI202ViewModel()
        {
            this.BtnUpLoad = new RelayTCommand<UI202ViewModel>(_logic.Test);
        }
    }

    /// <summary>
    /// List Item
    /// </summary>
    public class UI202Item
    { 
        
    }
}
