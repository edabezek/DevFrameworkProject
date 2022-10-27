using DevFramework.Core.DataAccess;
using DevFramework.Core.DataAccess.EntityFramework;
using DevFramework.Core.DataAccess.NHibernate;
using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.Concrete.Managers;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.DataAccess.Concrete.EntityFramework;
using DevFramework.Northwind.DataAccess.Concrete.NHibernate.Helpers;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.Business.DependencyResolvers.Ninject
{
    public class BusinnesModule : NinjectModule
    {
        public override void Load()
        {
            //Productservice isterse ProductManager ver
            //bu her istekte newlenir, inSingletonScope kullanarak ProductManager instance'ı oluşturulduğunda-içinde değer taşımaz,metot çağırımları gerçekleştirir- herkes aynı instance'ı kulanacak
            Bind<IProductService>().To<ProductManager>().InSingletonScope();
            Bind<IProductDal>().To<EFProductDal>().InSingletonScope();

            Bind<IUserService>().To<UserManager>().InSingletonScope();
            Bind<IUserDal>().To<EFUserDal>().InSingletonScope();



            //Queryable kullanmak istersek 
            Bind(typeof(IQueryableRepository<>)).To(typeof(EFQueryableRepositoryBase<>));//NHQueryableRepositoryBase
            //Bind<DbContext>().To<NorthwindContext>();//queryable aynı zamanda context de ister ,EF yüklememek için commentledim

            //Nhibernate kullamak istersek
            Bind<NHinernateHelper>().To<SqlServerHelper>();
        }
    }
}
