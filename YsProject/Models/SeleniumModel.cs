using Spire.Pdf.Exporting.XPS.Schema;
using System.Collections.Generic;
using System.Windows.Controls;

namespace YsProject.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class SeleniumModel
    {
        /// <summary>
        /// 数值
        /// </summary>
        public int PointX { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public int PointY { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public string StartUrl { get; set; }

        public List<SeleniumMenuItem> MenuList { get; set; }

        public Dictionary<string, SeleniumViewItem> ViewList { get; set; }
    }

    public class SeleniumMenuItem
    {
        public string Case { get; set; }
        public string View { get; set; }
        public string ViewName { get; set; }
        public string Event { get; set; }
    }

    public class SeleniumViewItem
    {
        public List<SeleniumControlItem> ControlList { get; set; }

        public Dictionary<string, SeleniumEventItem> EventList { get; set; }
    }

    public class SeleniumControlItem
    {
        public string No { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class SeleniumEventItem
    {
        public string Id { get; set; }
        public string Event { get; set; }
    }
}
