using System.Collections.ObjectModel;
using YsTool.ViewModels.Base;

namespace YsTool.ViewModels
{
    /// <summary>
    /// 浏览器模拟
    /// </summary>
    public class UI202ViewModel : BaseViewModel
    {
        /// <summary>
        /// URL
        /// </summary>
        public ObservableCollection<UI202Item> ResultList { get; set; }
    }

    /// <summary>
    /// List Item
    /// </summary>
    public class UI202Item
    { 
    }
}
