using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Data.Entity.ModelConfiguration;
using YsProject.Models.DB;
using MySql.Data.EntityFramework;
using YsProject.Utils;

namespace YsProject.Models
{

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class EFCoreDbContext : DbContext
    {
        public EFCoreDbContext() : base("name=MysqlConnectionString")
        {
            Database.SetInitializer<EFCoreDbContext>(null);
            Database.Log = x =>
            {
                LogUtility.WriteInfo(x.LastIndexOf("\r\n") > 0 ? x.Remove(x.LastIndexOf("\r\n"), 2) : x);
            };
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // 排他
            modelBuilder.Properties().Where(p => p.Name == "UpdateTime").Configure(p => p.IsConcurrencyToken());

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
        }

        public virtual DbSet<TB_User> TB_User { get; set; }
        public virtual DbSet<TB_Type> TB_Type { get; set; }

        //private readonly string _connectionString;

        //public EFCoreDbContext(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseMySQL(_connectionString);

        //Enable-Migrations
        //Add-Migration
        //Update-Database
    }
}
