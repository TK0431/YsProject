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
    [Table("tb_function"), Serializable]
    public class TB_Function : BaseTable
    {
        [Display(Name = "项目号"), Required, StringLength(20)]
        public string ProjectCD { get; set; }

        [Display(Name = "机能组", Order = 1), StringLength(20)]
        public string Group { get; set; }

        [Display(Name = "机能ID", Order = 2), Required, StringLength(10)]
        public string CD { get; set; }

        [Display(Name = "机能名", Order = 3), StringLength(20)]
        public string Name { get; set; }

        [Display(Name = "区分", Order = 4), StringLength(10)]
        public string Type { get; set; }

        [Display(Name = "纳品日", Order = 5)]
        public DateTime? DateEnd { get; set; }
    }

    public class Map_TB_Function : EntityTypeConfiguration<TB_Function>
    {
        public Map_TB_Function() : base()
        {
            // Index
            Property(t => t.ProjectCD).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("TB_Function_Index1", 0)));
            Property(t => t.CD).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("TB_Function_Index1", 1)));
        }
    }
}
