using DevFramework.Northwind.Business.Concrete.Managers;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;


namespace DevFramework.Northwind.Business.Tests
{
    [TestClass]
    public class ProductManagerTests
    {
        [ExpectedException(typeof(ValidationException))]//bu testin geçebilmesi için ex fırlatmalı,biz de burada exception beklediğimizi belirttik.
        [TestMethod]
        public void Product_validation_check()
        {
            //IProductDal'a ihtiyacımız var,moq concrete oluşturacak
            Mock<IProductDal> mock = new Mock<IProductDal>();//dataacces ref
            ProductManager productManager = new ProductManager(mock.Object);//businnes ref ,ProductManager içi IPRoductDal ister,biz mock vereceğiz

            //kurallara uygun olmayan nesne yazdık , bize bu durumda validation ex fırlatması gerekiyor.
            //product validator'da kuralları belirtmiştik, burada boş olmama kuralları var,biz boş gönderiyoruz.
            //veritabanına gidemeden -onentry- ex fırlatacak
            productManager.Add(new Product());
        }
    }
}
