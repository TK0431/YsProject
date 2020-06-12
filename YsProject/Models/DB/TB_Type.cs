using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
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

        [Display(Name = "值"), Required, StringLength(3)]
        public string Value { get; set; }

        [Display(Name = "语言"), Required, StringLength(1)]
        public string Language { get; set; }

        [Display(Name = "描述"), StringLength(20)]
        public string Name { get; set; }
    }

    public class Map_TB_Type : EntityTypeConfiguration<TB_Type>
    {
        public Map_TB_Type():base()
        {
            // index
            Property(t => t.Type).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("TB_Type_Index1", 0) { IsUnique = true }));
            Property(t => t.Value).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("TB_Type_Index1", 1) { IsUnique = true }));
            Property(t => t.Language).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("TB_Type_Index1", 2) { IsUnique = true }));
        }
    }
}
