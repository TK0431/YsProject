using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace YsProject.Models.DB
{
    /// <summary>
    /// User Table
    /// </summary>
    [Table("tb_user"), Serializable]
    public class TB_User : BaseTable
    {
        [Display(Name = "员工号"), Required, StringLength(10)]
        public string CD { get; set; }

        [Display(Name = "用户名"), Required, StringLength(10)]
        public string Name { get; set; }

        [Display(Name = "密码"), StringLength(20)]
        public string Password { get; set; }

        [Display(Name = "IP"), StringLength(15)]
        public string IP { get; set; }

        [Display(Name = "组号"), StringLength(10)]
        public string GroupId { get; set; }

        [Display(Name = "权限"), Required]
        public int Level { get; set; }
    }

    public class Map_TB_User : EntityTypeConfiguration<TB_User>
    {
        //public void Configure(EntityTypeBuilder<TB_User> builder)
        //{
        //    // 唯一索引
        //    builder.HasIndex(p => p.CD).IsUnique();
        //    builder.HasIndex(p => p.Name);
        //    builder.HasIndex(p => p.IP);
        //}
    }
}
