using DevFramework.Northwind.DataAccess.Concrete.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DevFramework.Northwind.DataAccess.Tests.EntityFrameworkTests
{
    [TestClass]
    public class EntityFrameworkTest
    {
        [TestMethod]
        public void Get_all_returns_all_product()//EfProductDal'da bütün ürünlerin listelenip listelenmediğini test edeceğiz
        {
            EFProductDal productDal = new EFProductDal();
            //entity ve core katmanıda da referans veriyoruz
            var result = productDal.GetList();//veritabanına bağlanacağız ve productların gelip gelmediğine bakacağız

            //Assert.IsNotNull(result); boş geliyor mu kontrol edebiliriz.
            Assert.AreEqual(78, result.Count);//veritabanında productda 78 satır var,yaptığımız sorgudan gelenle kıyaslıyoruz. 
        }
        [TestMethod]
        public void Get_all_with_parameter_returns_filtered_products()//EfProductDal'da parametre ile gelen değerleri test edeceğiz
        {
            EFProductDal productDal = new EFProductDal();
            var result = productDal.GetList(p=>p.ProductName.Contains("ab"));//içerisinde ab geçenleri listele dedik 
            //var result = productDal.GetList(p=>p.ProductName.Contains("ab")&&p.); diyerek bakabiliriz
            Assert.AreEqual(4, result.Count);//db de 4 tane var 
        }
    }
}
