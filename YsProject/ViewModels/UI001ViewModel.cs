using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using YsProject.Logics;
using YsProject.Models.DB;
using YsTool.ViewModels.Base;

namespace YsProject.ViewModels
{
    /// <summary>
    /// 用户界面
    /// </summary>
    public class UI001ViewModel : BaseViewModel
    {
        /// <summary>
        /// Logic
        /// </summary>
        private UI001Logic _logic = new UI001Logic();

        #region 属性

        /// <summary>
        /// 项目
        /// </summary>
        public ObservableCollection<TB_Project> ProjectItems { get; set; } = new ObservableCollection<TB_Project>();

        /// <summary>
        /// 选中项目
        /// </summary>
        public TB_Project SelectedProjectItem { get; set; }

        /// <summary>
        /// 工程选择后可点击
        /// </summary>
        public bool EnableFlg { get; set; }

        /// <summary>
        /// 上传Excel
        /// </summary>
        public ICommand ExcelUpload { get; set; }

        #endregion

        public UI001ViewModel()
        {
            this.ExcelUpload = new RelayTCommand<UI001ViewModel>(_logic.UploadExcel);
            //this.BtnFileReplace = new RelayTCommand<UI002ViewModel>(_logic.FileReplace);

            _logic.GetProjects(this);
        }
    }

    /// <summary>
    /// 一览
    /// </summary>
    public class UI002Item
    {
        /// <summary>
        /// 员工号
        /// </summary>
        public string Cd { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 组
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 删除
        /// </summary>
        public string Delete { get; set; }
    }
}
