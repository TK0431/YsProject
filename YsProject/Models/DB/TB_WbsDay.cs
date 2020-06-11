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
    [Table("tb_wbsday"), Serializable]
    public class TB_WbsDay : BaseTable
    {
        [Display(Name = "项目号"), Required, StringLength(20)]
        public string ProjectCD { get; set; }

        [Display(Name = "机能ID"), Required, StringLength(10)]
        public string CD { get; set; }

        [Display(Name = "作业种类"), Required, StringLength(2)]
        public string Type { get; set; }

        [Display(Name = "日期"), StringLength(7)]
        public string Day { get; set; }

        [Display(Name = "工时")]
        public decimal? WorkTime { get; set; }
    }

    public class Map_TB_WbsDay : EntityTypeConfiguration<TB_WbsDay>
    {
        public Map_TB_WbsDay() : base()
        {
            // Index
            Property(t => t.ProjectCD).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("TB_WbsDay_Index1", 0) { IsUnique = true}));
            Property(t => t.CD).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("TB_WbsDay_Index1", 1) { IsUnique = true }));
            Property(t => t.Type).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("TB_WbsDay_Index1", 2) { IsUnique = true }));
        }
    }
}
