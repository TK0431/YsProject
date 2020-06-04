using System;
using System.ComponentModel.DataAnnotations;

namespace YsProject.Models.DB
{
    /// <summary>
    /// BaseTabele
    /// </summary>
    [Serializable]
    public class BaseTable
    {
        [Display(Name = "ID"), Key]
        public int Id { get; set; }

        [Display(Name = "删除FLG")]
        public bool DelFlg { get; set; }

        [Display(Name = "登录者CD"), Required, StringLength(10)]
        public string InserterCd { get; set; }

        [Display(Name = "登录日时")]
        public DateTime InserteTime { get; set; }

        [Display(Name = "更新者CD"), Required, StringLength(10)]
        public string UpdaterCd { get; set; }

        [Display(Name = "更新日时")]
        public DateTime UpdateTime { get; set; }
    }
}
