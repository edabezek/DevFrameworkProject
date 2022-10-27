using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevFramework.Northwind.MvcWebUI.Controllers
{
    public class ProductController : Controller
    {
        //İş katmanında yazılmış olan manager'a ulaşmak için di
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: Product/Index
        public ActionResult Index()
        {
            var model = new ProductListViewModel
            {
                Products = _productService.GetAll()
            };
            return View(model);
        }
        public string Add()
        {
            _productService.Add(new Product 
            { 
                CategoryId = 1, 
                ProductName = "Gsm",
                QuantityPerUnit = "1", 
                UnitPrice = 21 
            });
            return "Added";
        }
        //product/addupdate
        public string AddUpdate()//ProductManager'deki TransactionalOperation işlemini yapacağız.
        {
            _productService.TransactionalOperation(new Product 
            {
                CategoryId = 1,//kurallara uygun product ekliyoruz
                ProductName = "Computer",
                QuantityPerUnit = "1",
                UnitPrice = 21
            }, new Product
            {
                CategoryId = 1,//kurallara uygun olmayan product ekliyoruz
                ProductName = "Computer 2",
                QuantityPerUnit = "1",
                UnitPrice = 10
            });
            return "Done";
        }

    }
}