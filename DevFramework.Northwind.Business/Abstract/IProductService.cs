using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.Business.Abstract
{
    [ServiceContract]//bunu wcf servislerde ,service olarak kullanabilmek için belirtiyoruz.
    public interface IProductService
    {
        [OperationContract] //her bir metodun operasyon nesnesi olduğunu anlatıyoruz.
        List<Product> GetAll();
        [OperationContract]
        Product GetById(int id);
        [OperationContract]
        Product Add(Product product);
        [OperationContract]
        Product Update(Product product);
        [OperationContract]
        Product Delete(Product product);
        [OperationContract]
        //Transaction işlemleri için metot,iki tane product gönderiyoruz
        void TransactionalOperation(Product product1, Product product2);
    }
}
