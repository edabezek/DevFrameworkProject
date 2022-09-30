using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Concrete.EntityFramework.Mapping
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            ToTable(@"Categories", @"dbo");//tablo,şema
            HasKey(x => x.CategoryId);
            //kolonları bağlayalım
            Property(x => x.CategoryId).HasColumnName("CategoryID");
            Property(x => x.CategoryName).HasColumnName("CategoryName");
            
            //Property(x => x.ProductName).HasColumnName("ProductName");
            //Property(x => x.QuantityPerUnit).HasColumnName("QuantityPerUnit");
            //Property(x => x.UnitPrice).HasColumnName("UnitPrice");
        }
    }
}
