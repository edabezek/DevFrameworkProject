using DevFramework.Core.DataAccess.NHibernate;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.ComplexTypes;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Concrete.NHibernate
{
    public class NHProductDal : NHEntityRepositoryBase<Product>, IProductDal
    {
        private NHinernateHelper _nHinernateHelper;
        public NHProductDal(NHinernateHelper nHinernateHelper) : base(nHinernateHelper)
        {
            _nHinernateHelper = nHinernateHelper;   
        }

        public List<ProductDetail> GetProductDetail()
        {
            using (var session=_nHinernateHelper.OpenSession())
            {
                var result = from p in session.Query<Product>()
                             join c in session.Query<Category>() on p.CategoryId equals c.CategoryId
                             select new ProductDetail { ProductId = p.ProductId, CategoryName = c.CategoryName, ProductName = p.ProductName };
                return result.ToList();
            }
        }
    }
}
