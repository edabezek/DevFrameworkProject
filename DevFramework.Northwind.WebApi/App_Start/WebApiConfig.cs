using DevFramework.Northwind.WebApi.MessageHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DevFramework.Northwind.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //api'de authorization çalışması için MessageHandler'ımızın configurasyonunu yaptık.
            config.MessageHandlers.Add(new AuthenticationHandler());//yapılan her isteğin önünde bu servis çalışacak
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
