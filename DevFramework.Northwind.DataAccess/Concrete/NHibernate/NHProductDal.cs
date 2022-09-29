using DevFramework.Core.DataAccess.NHibernate;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Concrete.NHibernate
{
    public class NHProductDal : NHEntityRepositoryBase<Product>, IProductDal
    {
        public NHProductDal(NHinernateHelper nHinernateHelper) : base(nHinernateHelper)
        {
        }
    }
}
