using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.DependencyResolvers.Ninject;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Burada IProductService'i Servis ediyoruz.
/// </summary>
public class ProductService : IProductService
{
    public ProductService()//buraya service'i enjekte etmememizin sebebi SOA standartlarına göre parametreli constructor'un olmaması.
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private IProductService _productService=InstanceFactory.GetInstance<IProductService>(); 

    public Product Add(Product product)
    {
        return _productService.Add(product);
    }

    public Product Delete(Product product)
    {
        throw new NotImplementedException();
    }

    public List<Product> GetAll()
    {
        return _productService.GetAll();    
    }

    public Product GetById(int id)
    {
        return _productService.GetById(id);
    }

    public void TransactionalOperation(Product product1, Product product2)
    {
        _productService.TransactionalOperation(product1, product2); 
    }

    public Product Update(Product product)
    {
        return _productService.Update(product); 
    }
}