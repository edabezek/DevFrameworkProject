using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.PostSharp.AuthorizationAspects
{
    [Serializable]
    public class SecuredOperation : OnMethodBoundaryAspect//Methodun başında çalışacağı için onmethod ekliyoruz.
    {
        public string Roles { get; set; }//Admin yada Editor 
        public override void OnEntry(MethodExecutionArgs args)//eğer metoda girmeye çalışan kullanıcı bizim rollerimize sahip ise işlemi yapmasına izin ver diyeceğiz.
        {
            string[] roles = Roles.Split(',');//Roles gelirken çoğul gelebilir(Admin yada Editor ),bunları virgül ile ayırıyoruz.

            bool isAuthorized = false;

            for (int i = 0; i < roles.Length; i++)//bütün rolleri gez
            {
                if (System.Threading.Thread.CurrentPrincipal.IsInRole(roles[i])) //System.Threading.Thread.CurrentPrincipal şuanki kullanıcı ,burada eğer kullanıcı Admin rolune sahipse veya Editor'e sahipse isAuthorise'i true yap,erişebilsin.
                {
                    isAuthorized = true;
                }
            }

            if (isAuthorized==false)//yetkisi yoksa
            {
                throw new SecurityException("You are not authorized!");
            }
            
        }
    }
}
