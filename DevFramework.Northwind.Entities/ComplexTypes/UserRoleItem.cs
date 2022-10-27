using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.Entities.ComplexTypes
{
    public  class UserRoleItem  //user'ın dbdeki rolunu join'leyecek
    {
        //getirmek istediğimiz bilgileri tanımlıyoruz.
        public string  RoleName { get; set; }
    }
}
