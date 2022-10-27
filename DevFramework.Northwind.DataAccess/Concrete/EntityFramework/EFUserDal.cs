using DevFramework.Core.DataAccess.EntityFramework;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.ComplexTypes;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Concrete.EntityFramework
{
    public class EFUserDal : EFEntityRepositoryBase<User, NorthwindContext>, IUserDal
    {
        //UserRoles ve Roles tablosuna join işlemi
        public List<UserRoleItem> GetUserRoles(User user)
        {
            using (NorthwindContext context=new NorthwindContext())
            {
                var result = from ur in context.UserRoles
                             join r in context.Roles
                             on ur.Id equals user.Id
                             where ur.Id == user.Id
                             select new UserRoleItem { RoleName = r.Name };
                return result.ToList();
            }
        }
    }
}
