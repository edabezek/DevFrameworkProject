using AutoMapper;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.Business.DependencyResolvers.Ninject
{
    public class AutoMapperModule : NinjectModule//mappingleri burada create edip kullanacağız.
    {
        public override void Load()
        {
            //InSingletonScope 
            Bind<IMapper>().ToConstant(CreateConfiguration().CreateMapper()).InSingletonScope();
        }
        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //AutoMapperHelper'deki gibi tek tek yapmak yerine gruplayarak yapacağız.
                cfg.AddProfiles(GetType().Assembly);//uygulamanın içinde profile olan herşeyi ekle
            });
            return config;  
        }
    }
}
