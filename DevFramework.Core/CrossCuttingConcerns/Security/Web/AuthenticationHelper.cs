using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace DevFramework.Core.CrossCuttingConcerns.Security.Web
{
    public class AuthenticationHelper
    {
        //expiration cookie'de ne kadar kalacağı,rememberMe kişi hatırla derse bunu tutacağız
        public static void CreateAuthCookie(Guid id,string userName,string email,DateTime expiration, string[] roles,bool rememberMe,string firstName,string lastName)
        {
            //form auth'da bir tane ticket oluşturuluyor ve bu cookie'de şifreli olarak tutuluyor
            var authTicket = new FormsAuthenticationTicket(1, userName, DateTime.Now, expiration, rememberMe, CreateAuthTags(email, roles, firstName, lastName, id));
            //
            string encTicket =FormsAuthentication.Encrypt(authTicket);
            //encript edilmiş ticket'ı cookie'e basma
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName,encTicket));//http cookie olarak cookielere eklenmesini sağlayacağız.FormsAuthentication.FormsCookieName ,FormsAuthentication ile tut.
        }

        private static string CreateAuthTags(string email, string[] roles, string firstName, string lastName, Guid id)
        {
            //email, roles, firstName, lastName, id değerlerini formatlı bir şekilde göndermeliyiz
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(email);
            stringBuilder.Append("|");//emaillerin bittiğini arasına ekleyerek belirtip ayırıyoruz.

            for (int i = 0; i < roles.Length; i++)//roller dizi şeklinde geliyordu
            {
                stringBuilder.Append(roles[i]);//her rolu stringBuilder'a ekliyoruz.
                //Admin,Editor olacak
                if (i<roles.Length-1)//her rolun arasına eklenecek olan virgülün, en sona eklememesi için bunu yazıyoruz.
                {
                    stringBuilder.Append(",");//her rolun arasına ekleyecek
                }
            }
            stringBuilder.Append("|");//rol bitti
            stringBuilder.Append(firstName);
            stringBuilder.Append("|");
            stringBuilder.Append(lastName);
            stringBuilder.Append("|");
            stringBuilder.Append(id);

            return stringBuilder.ToString();
        }
    }
}
