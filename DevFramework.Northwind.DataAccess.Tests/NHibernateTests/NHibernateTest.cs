using DevFramework.Northwind.DataAccess.Concrete.EntityFramework;
using DevFramework.Northwind.DataAccess.Concrete.NHibernate;
using DevFramework.Northwind.DataAccess.Concrete.NHibernate.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DevFramework.Northwind.DataAccess.Tests.NHibernateTests
{
    [TestClass]
    public class NHibernateTest
    {
        [TestMethod]
        public void Get_all_returns_all_product()
        {
            NHProductDal productDal = new NHProductDal(new SqlServerHelper());
            
            var result = productDal.GetList();

          
            Assert.AreEqual(78, result.Count);
        }
        [TestMethod]
        public void Get_all_with_parameter_returns_filtered_products()
        { 
            NHProductDal productDal = new NHProductDal(new SqlServerHelper());
            var result = productDal.GetList(p=>p.ProductName.Contains("ab")); 
           
            Assert.AreEqual(4, result.Count);
        }
    }
}
