using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.DependencyResolvers.Ninject;
using DevFramework.Northwind.Business.ServiceContracts.Wcf;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Sistemin bir kısmını dış dünyaya açmak için bu şekilde yapılabilir.Businnestan örnek kullanım için yapıldı.
/// </summary>
public class ProductDetailService : IProductDetailService
{
    IProductService _productService=InstanceFactory.GetInstance<IProductService>();
    public List<Product> GetAll()
    {
        return _productService.GetAll();
    }
}