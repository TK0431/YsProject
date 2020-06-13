using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using YsProject.Logics;
using YsProject.Models.DB;
using YsTool.ViewModels.Base;

namespace YsProject.ViewModels
{
    /// <summary>
    /// 设置
    /// </summary>
    public class WD001ViewModel : BaseViewModel
    {
        /// <summary>
        /// Logic
        /// </summary>
        private WD001Logic _logic = new WD001Logic();

        #region 属性

        /// <summary>
        /// 工程选择后可点击
        /// </summary>
        public string UserName { get; set; } = App.LoginUser?.Name;

        /// <summary>
        /// 工程选择后可点击
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 工程选择后可点击
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 上传Excel
        /// </summary>
        public ICommand Login { get; set; }

        #endregion

        public WD001ViewModel()
        {
            this.Login = new RelayTCommand<WD001ViewModel>(_logic.Login);
        }
    }
}
