using DevFramework.Core.CrossCuttingConcerns.Security.Web;
using DevFramework.Core.Utilities.Mvc.InfraStructure;
using DevFramework.Northwind.Business.DependencyResolvers.Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

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

        //Asp.net mvc de kullanýcýnýn identity bilgilerini eriþilebilir olduðu zaman onu kullanabiliriz.
        public override void Init()
        {
            PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }
        //kiþinin auth bilgilerinin eriþilebilir olduðu zamana dek gelir.
        private void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            try
            {
                //önce cookie'ye eriþmeye çalýþacaðýz,
                //Þuanki requestin cookielerine eriþ FormsAuthentication'nin cookisine
                var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie == null)
                {
                    return;
                }
                var encTicket = authCookie.Value;
                if (String.IsNullOrEmpty(encTicket))
                {
                    return;
                }
                var ticket = FormsAuthentication.Decrypt(encTicket);

                //SecurityUtilities , ticket'ý identity'e çeviriyordu
                var securityUtilities = new SecurityUtilities();
                var identity = securityUtilities.FormsAuthenticationTicketToIdentity(ticket);
                //princple oluþturacaðýz
                var princple = new GenericPrincipal(identity, identity.Roles);

                //web uygulamalarýnda kullanmak için ,mvc için
                HttpContext.Current.User = princple;
                //backend'in de kullanabilmesi için
                Thread.CurrentPrincipal = princple;
            }
            catch (Exception)
            {
            }
            
        }
    }
}
