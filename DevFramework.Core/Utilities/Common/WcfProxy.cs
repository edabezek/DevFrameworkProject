using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Utilities.Common
{
    public class WcfProxy<T> //bunu Mvc'de çalıştıracağız.
    {
        public static T CreateChannel()
        {
            string baseAddress = ConfigurationManager.AppSettings["ServiceAddress"];
            //base adresin içinden servisi çekeceğiz.bu sürekli değişecek
            //localhost:1111/ProductService.svc olarak servisi çağırıyoruz.
            //biz bunu mvc'de localhost:1111/{0}.svc olarak tutacağız,generic olması için
            //baseAddress : bizim link
            //bizim sevisimiz ise IProductService ,I'yi keseceğiz ,1den itibaren al diyoruz.
            string adress = string.Format(baseAddress, typeof(T).Name.Substring(1));

            var binding = new BasicHttpBinding();
            //binding'i kullanarak bir cahnnel üret
            var channel = new ChannelFactory<T>(binding,adress);

            return channel.CreateChannel();//Proxy nesne üretecek
        }
    }
}
