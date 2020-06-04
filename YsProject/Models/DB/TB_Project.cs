using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace YsProject.Models.DB
{
    /// <summary>
    /// User Table
    /// </summary>
    [Table("tb_project"), Serializable]
    public class TB_Project : BaseTable
    {
        [Display(Name = "项目号"), Required, StringLength(20)]
        public string CD { get; set; }

        [Display(Name = "项目名"), Required, StringLength(20)]
        public string Name { get; set; }

        [Display(Name = "项目期间(开始)")]
        public DateTime DateStart { get; set; }

        [Display(Name = "项目期间(终了)")]
        public DateTime DateEnd { get; set; }
    }

    public class Map_TB_Project : EntityTypeConfiguration<TB_Project>
    {
        //public void Configure(EntityTypeBuilder<TB_Project> builder)
        //{
        //    // 唯一索引
        //    builder.HasIndex(p => p.CD).IsUnique();
        //}
    }
}
