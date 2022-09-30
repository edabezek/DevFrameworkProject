using DevFramework.Core.DataAccess.EntityFramework;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.ComplexTypes;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Concrete.EntityFramework
{
    public class EFProductDal : EFEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public List<ProductDetail> GetProductDetail()//joinden gelen 
        {
            using (NorthwindContext context=new NorthwindContext())
            {
                //contex deki Products ile Categories i join et,p'nin categoryId eşit olacak c'nin categoryId'sine
                //select ile eşleştiriyoruz
                var result = from p in context.Products
                             join c in context.Categories on p.CategoryId equals c.CategoryId
                             select new ProductDetail { ProductId = p.ProductId, ProductName = p.ProductName, CategoryName = c.CategoryName };
                return result.ToList();
            }
            
        }
    }
}
