using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace DevFramework.Core.CrossCuttingConcerns.Security.Web
{
    public class SecurityUtilities
    {
        public Identity FormsAuthenticationTicketToIdentity(FormsAuthenticationTicket ticket)//elimizde bir ticket-cookie var onu parçalayıp Identity oluşturup onu return ettik
        {
            //bir identity oluşturuyoruz.
            var identity = new Identity()
            {
                Id = SetId(ticket),//tickettan ıd'yi okuyacağız,çekeceğiz
                Name = SetName(ticket),
                Email = SetEmail(ticket),
                Roles = SetRole(ticket),
                FirstName = SetFirstName(ticket),
                LastName = SetLastName(ticket),
                AuthenticationType = SetAuthenticationType(),
                IsAuthenticated = SetIsAuthenticated()
            }; 
            return identity;
        }

        private bool SetIsAuthenticated()
        {
            return true;
        }

        private string SetAuthenticationType()
        {
            return "Forms";
        }

        private string SetLastName(FormsAuthenticationTicket ticket)
        {
            //UserData = CreateAuthTags
            string[] data = ticket.UserData.Split('|');
            //CreateAuthTags(email, roles, firstName, lastName, id)); firstName 3.sırada
            return data[3]; 
        }

        private string SetFirstName(FormsAuthenticationTicket ticket)
        {
            string[] data = ticket.UserData.Split('|');
            return data[2];
        }

        private string[] SetRole(FormsAuthenticationTicket ticket)
        {
            string[] data = ticket.UserData.Split('|');
            //StringSplitOptions.RemoveEmptyEntries kullanıcı boşluk girerse silinecek
            string[] roles = data[1].Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries);
            return roles;
        }

        private string SetEmail(FormsAuthenticationTicket ticket)
        {
            string[] data = ticket.UserData.Split('|');
            return data[0];
        }

        private string SetName(FormsAuthenticationTicket ticket)
        {
           return ticket.Name;
        }

        private Guid SetId(FormsAuthenticationTicket ticket)
        {
            string[] data = ticket.UserData.Split('|');
            return new Guid( data[4]);
        }
    }
}
