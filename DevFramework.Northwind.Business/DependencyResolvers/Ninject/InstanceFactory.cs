using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.Business.DependencyResolvers.Ninject
{
    public class InstanceFactory //IUserService için factory
    {
        public static T GetInstance<T>()
        {
            var kernel = new StandardKernel(new BusinnesModule(),new AutoMapperModule());
            return kernel.Get<T>();//gönderdiğimiz T için nesne üretecek
        }
    }
}
