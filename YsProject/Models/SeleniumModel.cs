using System.Collections.Generic;

namespace YsProject.Models
{
    /// <summary>
    /// Selenium Excel
    /// </summary>
    public class SeleniumModel
    {
        /// <summary>
        /// 启动URL
        /// </summary>
        public string StartUrl { get; set; }

        /// <summary>
        /// Menu数据
        /// </summary>
        public List<SeleniumMenuItem> MenuList { get; set; }

        /// <summary>
        /// 画面数据
        /// </summary>
        public Dictionary<string, SeleniumViewItem> ViewList { get; set; }
    }

    /// <summary>
    /// Menu数据
    /// </summary>
    public class SeleniumMenuItem
    {
        /// <summary>
        /// Case
        /// </summary>
        public string Case { get; set; }

        /// <summary>
        /// 画面Sheet名
        /// </summary>
        public string View { get; set; }

        /// <summary>
        /// 画面名
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// 事件ID
        /// </summary>
        public string Event { get; set; }
    }

    /// <summary>
    /// 画面数据
    /// </summary>
    public class SeleniumViewItem
    {
        /// <summary>
        /// 控件List
        /// </summary>
        public List<SeleniumControlItem> ControlList { get; set; }

        /// <summary>
        /// 事件List
        /// </summary>
        public Dictionary<string, List<SeleniumEventItem>> EventList { get; set; }
    }

    /// <summary>
    /// 控件
    /// </summary>
    public class SeleniumControlItem
    {
        /// <summary>
        /// No
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 控件名
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 事件
    /// </summary>
    public class SeleniumEventItem
    {
        /// <summary>
        /// 控件ID
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 事件
        /// </summary>
        public string Event { get; set; }
    }
}
