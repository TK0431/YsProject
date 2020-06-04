using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace YsProject.Models.DB
{
    /// <summary>
    /// User Table
    /// </summary>
    [Table("tb_group"), Serializable]
    public class TB_Group : BaseTable
    {
        [Display(Name = "组号"), Required, StringLength(10)]
        public string CD { get; set; }

        [Display(Name = "组名"), Required, StringLength(10)]
        public string Name { get; set; }

        [Display(Name = "期间(From)"), StringLength(20)]
        public DateTime DateFrom { get; set; }

        [Display(Name = "密码(To)"), StringLength(20)]
        public DateTime DateEnd { get; set; }

        [Display(Name = "组担当号"), Required, StringLength(10)]
        public string UserCD { get; set; }
    }

    public class Map_TB_Group : EntityTypeConfiguration<TB_Group>
    {
        //public void Configure(EntityTypeBuilder<TB_Group> builder)
        //{
        //    // 索引
        //    builder.HasIndex(p => new { p.CD, p.Name });
        //}
    }
}
