using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace YsProject.Models.DB
{
    /// <summary>
    /// User Table
    /// </summary>
    [Table("tb_type"), Serializable]
    public class TB_Type : BaseTable
    {
        [Display(Name = "类型"), Required, StringLength(2)]
        public string Type { get; set; }

        [Display(Name = "值"), Required, StringLength(2)]
        public string Value { get; set; }

        [Display(Name = "描述"), StringLength(20)]
        public string Name { get; set; }
    }

    public class Map_TB_Type : EntityTypeConfiguration<TB_Type>
    {
        //public void Configure(EntityTypeBuilder<TB_Type> builder)
        //{
        //    // 唯一索引
        //    builder.HasIndex(p => new { p.Type, p.Value }).IsUnique();
        //}
    }
}
