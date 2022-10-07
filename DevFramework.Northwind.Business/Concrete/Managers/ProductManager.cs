using DevFramework.Core.CrossCuttingConcerns.Validation.FluentValidation;
using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.ValidationRules.FluentValidation;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFramework.Core.Aspects;
using DevFramework.Core.Aspects.PostSharp;
using DevFramework.Core.DataAccess;

namespace DevFramework.Northwind.Business.Concrete.Managers
{
    public class ProductManager : IProductService
    {
        
        private IProductDal _productDal;
        //private IQueryableRepository<Product> _queryable;//Queryable'ı kullanmak istersek 
        public ProductManager(IProductDal productDal/*, IQueryableRepository<Product> queryable*/)//IProductDal vererek EF kullanacağız
        {
            _productDal = productDal;
            //_queryable = queryable;   
        }
        [FluentValidationAspect(typeof(ProductValidator))]//bize gelen product'ı ProductValidator kullanarak validate edeceğiz.
        public Product Add(Product product)
        {
            //ValidatorTool.FluentValidate(new ProductValidator(), product);
            return _productDal.Add(product);
        }

        public Product Delete(Product product)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _productDal.GetList();
            //Queryable
            //_queryable.Table.Where..
        }

        public Product GetById(int id)
        {
            return _productDal.Get(p=>p.ProductId==id); 
        }
        [FluentValidationAspect(typeof(ProductValidator))]
        public Product Update(Product product)
        {
            return _productDal.Update(product);
        }
    }
}
