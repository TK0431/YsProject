using System.Windows.Input;
using YsProject.Logics;
using YsTool.ViewModels.Base;

namespace YsProject.ViewModels
{
    /// <summary>
    /// 定义生成
    /// </summary>
    public class UI903ViewModel : BaseViewModel
    {
        /// <summary>
        /// Logic
        /// </summary>
        private UI903Logic _logic = new UI903Logic();

        /// <summary>
        /// 检索
        /// </summary>
        public string LeftTxt { get; set; }

        /// <summary>
        /// 检索
        /// </summary>
        public string RightTxt { get; set; }

        /// <summary>
        /// 检索
        /// </summary>
        public string In { get; set; }

        /// <summary>
        /// 检索
        /// </summary>
        public string Out { get; set; }

        /// <summary>
        /// 检索按钮
        /// </summary>
        public ICommand BtnGet { get; set; }

        /// <summary>
        /// 初期
        /// </summary>
        public UI903ViewModel()
        {
            BtnGet = new RelayTCommand<UI903ViewModel>(_logic.GetOut);
        }
    }
}
