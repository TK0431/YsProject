using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace YsProject.Models.DB
{
    /// <summary>
    /// Function Table
    /// </summary>
    [Table("tb_wbstype"), Serializable]
    public class TB_WbsType : BaseTable
    {
        [Display(Name = "项目号"), Required, StringLength(20)]
        public string ProjectCD { get; set; }

        [Display(Name = "机能ID"), Required, StringLength(10)]
        public string CD { get; set; }

        [Display(Name = "作业种类"), Required, StringLength(2)]
        public string Type { get; set; }

        [Display(Name = "担当者"), StringLength(10)]
        public string UserCD { get; set; }

        [Display(Name = "预定开始")]
        public DateTime? DatePeFrom { get; set; }

        [Display(Name = "预定终了")]
        public DateTime? DatePeEnd { get; set; }

        [Display(Name = "实际开始")]
        public DateTime? DateReFrom { get; set; }

        [Display(Name = "实际终了")]
        public DateTime? DateReEnd { get; set; }

        [Display(Name = "进度")]
        public decimal? Percent { get; set; }

        [Display(Name = "备考")]
        public string Back { get; set; }
    }

    public class Map_TB_WbsType : EntityTypeConfiguration<TB_WbsType>
    {
        public Map_TB_WbsType() : base()
        {
            // Index
            Property(t => t.ProjectCD).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("TB_WbsType_Index1", 0) { IsUnique = true}));
            Property(t => t.CD).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("TB_WbsType_Index1", 1) { IsUnique = true }));
            Property(t => t.Type).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("TB_WbsType_Index1", 2) { IsUnique = true }));
        }
    }
}
