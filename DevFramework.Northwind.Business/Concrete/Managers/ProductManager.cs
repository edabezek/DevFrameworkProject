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
using System.Transactions;
using DevFramework.Core.Aspects.PostSharp.TransactionAspect;
using DevFramework.Core.Aspects.PostSharp.ValidationAspects;
using DevFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using DevFramework.Core.Aspects.PostSharp.CacheAspects;
using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using DevFramework.Core.Aspects.PostSharp.LogAspects;
using DevFramework.Core.Aspects.PostSharp.PerformanceAspects;
using DevFramework.Core.Aspects.PostSharp.AuthorizationAspects;
using AutoMapper;
using DevFramework.Core.Utilities.Mappings;

namespace DevFramework.Northwind.Business.Concrete.Managers
{
    //[LogAspect(typeof(FileLogger))]//bütün product metotları yazacak
    public class ProductManager : IProductService
    {
        
        private IProductDal _productDal;
        private IMapper _mapper;
        //private IQueryableRepository<Product> _queryable;//Queryable'ı kullanmak istersek 
        public ProductManager(IProductDal productDal,IMapper mapper/*, IQueryableRepository<Product> queryable*/)//IProductDal vererek EF kullanacağız
        {
            _productDal = productDal;
            _mapper = mapper;   
            //_queryable = queryable;   
        }
        [FluentValidationAspect(typeof(ProductValidator))]//bize gelen product'ı ProductValidator kullanarak validate edeceğiz.
        [CacheRemoveAspect(typeof(MemoryCacheManager))]//product ile ilgili bütün cacheleri silmeye yarayacak
        //[LogAspect(typeof(FileLogger))]
        public Product Add(Product product)
        {
            //ValidatorTool.FluentValidate(new ProductValidator(), product);
            return _productDal.Add(product);
        }

        public Product Delete(Product product)
        {
            throw new NotImplementedException();
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        //[LogAspect(typeof(DatabaseLogger))]//db'ye yazar
        //[LogAspect(typeof(FileLogger))]//dosyaya yazar
        [PerformanceCounterAspect(2)]
        //[SecuredOperation(Roles="Admin,Editor")]//Get all metodunu sadece Admin ve Editor'lar görecek
        //[SecuredOperation(Roles = "Admin,Editor,Student")]
        public List<Product> GetAll()
        {
            //performance aspect için
            //Thread.Sleep(3000) ile geciktirip bakabilirsin.
            //return _productDal.GetList();

            //Queryable
            //_queryable.Table.Where..

            //manuel mapping --serializable olması için yaptık- eğer bir kaç nesne mapping isterse böyle yapabiliriz.
            //return _productDal.GetList().Select(p => new Product
            //{
            //    CategoryId = p.CategoryId,
            //    ProductId = p.ProductId,    
            //    ProductName = p.ProductName,
            //    QuantityPerUnit = p.QuantityPerUnit,
            //    UnitPrice = p.UnitPrice
            //}).ToList();


            //ninject kullanmadan mapper
            //mapping kurallarını belirteceğiz  c:config
            //source,destination
            //List<Product> products = AutoMapperHelper.MapToSameTypeList<Product>(_productDal.GetList());
            //return products;

            //ninject module kullanarak auto mapper
            var products=_mapper.Map<List<Product>>(_productDal.GetList());   
            return products;    
        }



        public Product GetById(int id)
        {
            return _productDal.Get(p=>p.ProductId==id); 
        }
        //Transaction işlemleri
        [TransactionScopeAspect]
        [FluentValidationAspect(typeof(ProductValidator))]
        public void TransactionalOperation(Product product1, Product product2)
        {
            //using (TransactionScope scope=new TransactionScope())//bunu her metodun içinde yazmamız gerektiğinden Aspect olarak yazacağız.
            //{
            //    try//iki işlem de olmazsa iptal et
            //    {
            //        _productDal.Add(product1);
            //        //Businnes works
            //        _productDal.Update(product2);
            //        scope.Complete();  
            //    }
            //    catch 
            //    {
            //        scope.Dispose();
            //    }
            //}


            //yukaırdaki kod yerine aspect yazdık

            _productDal.Add(product1);//product1 kurallara uyan 
            //Businnes works
            _productDal.Update(product2);//product2 kurallara uymayan göndereceğiz
            //Sonuc olarak product1 eklendiğinde ama product2 eklenemediğinde ,product1 işlemini geri almasını istiyoruz.
        }

        [FluentValidationAspect(typeof(ProductValidator))]
        public Product Update(Product product)
        {
            return _productDal.Update(product);
        }
    }
}
