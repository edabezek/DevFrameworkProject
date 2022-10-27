using DevFramework.Northwind.DataAccess.Concrete.EntityFramework.Mapping;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DevFramework.Northwind.DataAccess.Concrete.EntityFramework
{
    public class NorthwindContext : DbContext 
    {
        public NorthwindContext()//Hazır veritabanı kullandığımız için migrationu kapatacağız,yani veritabanı var oluşturmasın diye
        {
            Database.SetInitializer<NorthwindContext>(null);   //migration datası vermiyoruz,veritabanının kod tarafından üretilmesini engellemek için 
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new CategoryMap());
        }

    }
}
