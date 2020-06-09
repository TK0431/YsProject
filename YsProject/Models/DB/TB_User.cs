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
    [Table("tb_user"), Serializable]
    public class TB_User : BaseTable
    {
        [Display(Name = "项目号"), Required, StringLength(20)]
        public string ProjectCD { get; set; }

        [Display(Name = "员工号", Order = 1), Required, StringLength(10)]
        public string CD { get; set; }

        [Display(Name = "用户名", Order = 2), Required, StringLength(10)]
        public string Name { get; set; }

        [Display(Name = "密码", Order = 3), Required, StringLength(32)]
        public string Password { get; set; }

        [Display(Name = "IP", Order = 4), StringLength(15)]
        public string IP { get; set; }

        [Display(Name = "组号", Order = 5), StringLength(10)]
        public string GroupId { get; set; }

        [Display(Name = "权限", Order = 6), Required]
        public int Level { get; set; }

        [Display(Name = "期间(开始)", Order = 7)]
        public DateTime? DateStart { get; set; }

        [Display(Name = "期间(终了)", Order = 8)]
        public DateTime? DateEnd { get; set; }
    }

    public class Map_TB_User : EntityTypeConfiguration<TB_User>
    {
        public Map_TB_User() : base()
        {
            // Index
            Property(t => t.ProjectCD).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("TB_User_Index1", 0) { IsUnique = true }));
            Property(t => t.CD).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("TB_User_Index1", 1) { IsUnique = true }));
            Property(t => t.IP).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("TB_User_Index2")));
        }
    }
}
