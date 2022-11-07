using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.DependencyResolvers.Ninject;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace DevFramework.Northwind.WebApi.MessageHandler
{
    public class AuthenticationHandler : DelegatingHandler//DelegatingHandler : message handler 
    {
        //db'den gelen verilerle
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var token = request.Headers.GetValues("Authorization").FirstOrDefault();
                if (token != null)
                {
                    byte[] data = Convert.FromBase64String(token);
                    string decodedString = Encoding.UTF8.GetString(data);
                    string[] tokenValues = decodedString.Split(':');  

                    //ninject bize user manager verecek
                    IUserService userService = InstanceFactory.GetInstance<IUserService>();

                    User user = userService.GetByUserNameAndPassword(tokenValues[0], tokenValues[1]);

                    if (user!=null)//böyle bir kullanıcı varsa
                    {
                        IPrincipal principal = new GenericPrincipal(new GenericIdentity(tokenValues[0]), userService.GetUserRoles(user).Select(u=>u.RoleName).ToArray());
                        Thread.CurrentPrincipal = principal;
                        HttpContext.Current.User = principal; 
                    }

                }
            }
            catch
            {
            }
            return base.SendAsync(request, cancellationToken);
        }
        //hard coded version
        //protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        //{
        //    //yapılan istekten token'ı yakalamaya-çekmeye çalışacağız
        //    try
        //    {
        //        //yapılan isteğin header'larından,Authorization şeklinde vereceği değeri çek ,ilkini al
        //        var token = request.Headers.GetValues("Authorization").FirstOrDefault();//auth token'ı çekiyoruz
        //        if (token!=null)//token bos gelmiyorsa token'ı okuruz
        //        {
        //            byte[] data = Convert.FromBase64String(token);
        //            string decodedString= Encoding.UTF8.GetString(data);
        //            //"engin:12345" şeklinde istiyoruz
        //            string[] tokenValues= decodedString.Split(':');  //ikisini ayırdık

        //            //şimdi bu kullanıcı var mı ona bakıyoruz,normalde db'den çekip bakılmalı-hard code 
        //            if (tokenValues[0]=="engin"&& tokenValues[1] == "12345")
        //            {
        //                //bu kişinin rollerini getirip,principal nesnesine atıyoruz.Admin olduğunu veriyoruz.
        //                IPrincipal principal = new GenericPrincipal(new GenericIdentity(tokenValues[0]), new[] {"Admin"});
        //                Thread.CurrentPrincipal = principal; //backend'deki identiy set edilmiş olur   
        //                HttpContext.Current.User = principal; //web api için
        //            }

        //        }
        //    }
        //    catch 
        //    {
        //    }
        //    return base.SendAsync(request, cancellationToken);
        //}
    }
}