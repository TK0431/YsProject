using System;
using System.CodeDom;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace YsProject.Models.DB
{
    /// <summary>
    /// User Table
    /// </summary>
    [Table("tb_project"), Serializable]
    public class TB_Project : BaseTable
    {
        [Display(Name = "项目号", Order = 1), Required, StringLength(20)]
        public string CD { get; set; }

        [Display(Name = "项目名", Order = 2), Required, StringLength(20)]
        public string Name { get; set; }

        [Display(Name = "项目期间(开始)", Order = 3)]
        public DateTime? DateStart { get; set; }

        [Display(Name = "项目期间(终了)", Order = 4)]
        public DateTime? DateEnd { get; set; }
    }

    public class Map_TB_Project : EntityTypeConfiguration<TB_Project>
    {
        public Map_TB_Project():base()
        {
            // Index
            Property(t => t.CD).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("TB_Project_Index1") { IsUnique = true }));
        }
    }
}
