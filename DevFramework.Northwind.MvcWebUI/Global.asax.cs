using DevFramework.Core.Utilities.Mvc.InfraStructure;
using DevFramework.Northwind.Business.DependencyResolvers.Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DevFramework.Northwind.MvcWebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Mvc'ye ControllerFactory olarak NinjectFactory kullanacaðýmýzý belirtiyoruz
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(new BusinnesModule()));
        }
    }
}
