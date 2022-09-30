using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Concrete.NHibernate.Mappings
{
    public class CategoryMap : FluentNHibernate.Mapping.ClassMap<Category>
    {
        public CategoryMap()
        {
            Table(@"Categories");
            LazyLoad();
            Id(x => x.CategoryId).Column("CategoryID");//primary key

            Map(x=>x.CategoryName).Column("CategoryName");
            //Map(x => x.ProductName).Column("ProductName");
            //Map(x => x.QuantityPerUnit).Column("QuantityPerUnit");
            //Map(x => x.UnitPrice).Column("UnitPrice");
        }
    }
}
