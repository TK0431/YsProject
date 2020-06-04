using System.Collections.ObjectModel;
using System.Windows.Input;
using YsProject.Logics;
using YsTool.ViewModels.Base;

namespace YsProject.ViewModels
{
    /// <summary>
    /// 用户界面
    /// </summary>
    public class UI002ViewModel : BaseViewModel
    {
        /// <summary>
        /// Logic
        /// </summary>
        private UI002Logic _logic = new UI002Logic();

        #region 属性

        /// <summary>
        /// 检索
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// 员工号
        /// </summary>
        public string Cd { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string IP
        {
            get
            {
                return IP1 + "." + IP2 + "." + IP3 + "." + IP4;
            }
        }

        public int IP1 { get; set; }

        public int IP2 { get; set; }

        public int IP3 { get; set; }

        public int IP4 { get; set; }

        /// <summary>
        /// 组ID
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// 一览
        /// </summary>
        public ObservableCollection<UI002Item> Items { get; set; } = new ObservableCollection<UI002Item>();

        /// <summary>
        /// 选中项目
        /// </summary>
        // public UI002Item SelectedItem { get; set; }

        /// <summary>
        /// 检索按钮
        /// </summary>
        public ICommand BtnSearch { get; set; }

        /// <summary>
        /// 获取当前IP
        /// </summary>
        public ICommand BtnGetIP { get; set; }

        /// <summary>
        /// 获取组
        /// </summary>
        public ICommand BtnGetGroup { get; set; }

        /// <summary>
        /// 保存
        /// </summary>
        public ICommand BtnSave { get; set; }

        /// <summary>
        /// 员工全局替换（文件）
        /// </summary>
        public ICommand BtnFileReplace { get; set; }

        /// <summary>
        /// 员工追加（文件）
        /// </summary>
        public ICommand BtnFileAdd { get; set; }

        /// <summary>
        /// 员工导出（文件）
        /// </summary>
        public ICommand BtnFileOut { get; set; }

        #endregion

        public UI002ViewModel()
        {
            this.BtnGetIP = new RelayTCommand<UI002ViewModel>(_logic.GetIP);
            this.BtnFileReplace = new RelayTCommand<UI002ViewModel>(_logic.FileReplace);

            _logic.GetUsers(this);
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
