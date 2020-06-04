using System.Collections.ObjectModel;
using System.Windows.Input;
using YsProject.Consts;
using YsProject.Logics;
using YsProject.Models;
using YsProject.Utility;
using YsTool.ViewModels.Base;

namespace YsProject.ViewModels
{
    /// <summary>
    /// 定义生成
    /// </summary>
    public class UI902ViewModel : BaseViewModel
    {
        /// <summary>
        /// Logic
        /// </summary>
        private UI902Logic _logic = new UI902Logic();

        /// <summary>
        /// 检索
        /// </summary>
        public string FileDictionary { get; set; }

        /// <summary>
        /// 检索
        /// </summary>
        public string In { get; set; }

        /// <summary>
        /// 检索
        /// </summary>
        public string Out { get; set; }

        /// <summary>
        /// 检索
        /// </summary>
        public bool ReverseFlg { get; set; }

        /// <summary>
        /// 检索
        /// </summary>
        public ObservableCollection<EnumItem> Items { get; set; } = new ObservableCollection<EnumItem>();

        /// <summary>
        /// 检索
        /// </summary>
        public EnumItem Item { get; set; }

        /// <summary>
        /// 检索按钮
        /// </summary>
        public ICommand BtnDictionary { get; set; }

        /// <summary>
        /// 检索按钮
        /// </summary>
        public ICommand BtnOut { get; set; }

        /// <summary>
        /// 初期
        /// </summary>
        public UI902ViewModel()
        {
            Items = new ObservableCollection<EnumItem>(typeof(EnumDevLang).GetList());
            foreach (EnumItem e in Items)
                if (e.Index == (int)EnumDevLang.CSHUP)
                {
                    Item = Items[1];
                    break;
                }

            this.BtnDictionary = new RelayTCommand<UI902ViewModel>(_logic.InputDictionary);
            this.BtnOut = new RelayTCommand<UI902ViewModel>(_logic.GetOut);
        }
    }
}
