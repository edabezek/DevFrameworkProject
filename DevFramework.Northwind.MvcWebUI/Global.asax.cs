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

            //Mvc'ye ControllerFactory olarak NinjectFactory kullanaca��m�z� belirtiyoruz
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(new BusinnesModule()));
        }

        //Asp.net mvc de kullan�c�n�n identity bilgilerini eri�ilebilir oldu�u zaman onu kullanabiliriz.
        public override void Init()
        {
            PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }
        //ki�inin auth bilgilerinin eri�ilebilir oldu�u zamana dek gelir.
        private void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            try
            {
                //�nce cookie'ye eri�meye �al��aca��z,
                //�uanki requestin cookielerine eri� FormsAuthentication'nin cookisine
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

                //SecurityUtilities , ticket'� identity'e �eviriyordu
                var securityUtilities = new SecurityUtilities();
                var identity = securityUtilities.FormsAuthenticationTicketToIdentity(ticket);
                //princple olu�turaca��z
                var princple = new GenericPrincipal(identity, identity.Roles);

                //web uygulamalar�nda kullanmak i�in ,mvc i�in
                HttpContext.Current.User = princple;
                //backend'in de kullanabilmesi i�in
                Thread.CurrentPrincipal = princple;
            }
            catch (Exception)
            {
            }
            
        }
    }
}
