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
        /// 项目
        /// </summary>
        public ObservableCollection<UI001DataItem> DataItems { get; set; } = new ObservableCollection<UI001DataItem>();

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
    /// Grid数据
    /// </summary>
    public class UI001DataItem
        {
        /// <summary>
        /// No
        /// </summary>
        public int rownum { get; set; }

        /// <summary>
        /// Group
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// CD
        /// </summary>
        public string CD { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// DateEnd
        /// </summary>
        public DateTime? DateEnd { get; set; }

        /// <summary>
        /// Work
        /// </summary>
        public string Work { get; set; }

        /// <summary>
        /// UserCD
        /// </summary>
        public string UserCD { get; set; }

        /// <summary>
        /// DatePeFrom
        /// </summary>
        public DateTime? DatePeFrom { get; set; }

        /// <summary>
        /// DatePeEnd
        /// </summary>
        public DateTime? DatePeEnd { get; set; }

        /// <summary>
        /// DateReFrom
        /// </summary>
        public DateTime? DateReFrom { get; set; }

        /// <summary>
        /// DateReEnd
        /// </summary>
        public DateTime? DateReEnd { get; set; }

        /// <summary>
        /// Percent
        /// </summary>
        public decimal? Percent { get; set; }

        /// <summary>
        /// Back
        /// </summary>
        public DateTime? Back { get; set; }
    }
}
