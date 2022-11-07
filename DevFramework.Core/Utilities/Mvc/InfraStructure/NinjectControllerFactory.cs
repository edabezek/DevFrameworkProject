using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace DevFramework.Core.Utilities.Mvc.InfraStructure
{//burada uygulamayı başlattığımızda adresten controller'ı bulup newleyen burası DefaultControllerFactory.Biz kendimizinkini kullanmasını istiyoruz NinjectControllerFactory.
    //NinjectControllerFactory'e birşeyi nasıl üreteceğini veriyoruz.
    //Product controller'ın çözümlenmeye ihtiyacı varsa onu kernel'den çözümle diyoruz.
    //kernel ile çözümleme işlemi 
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel _kernel;

        public NinjectControllerFactory(params INinjectModule[] module)
        {
            _kernel = new StandardKernel(module);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            //kernel'den yararlanarak çözümleme 
            //controller type boş ise boş dön,değilse-bir controller varsa- onu IController'a çevir kernel için
            //Diyelim product controller geldi , product controller'ı çözümlemek için kernel'e bak , kernel içinde Product Controller'a gidecek
            //yani NinjectController factory kullanacağımızı belittik
            return controllerType == null ? null : (IController) _kernel.Get(controllerType);
        }
    }
}
