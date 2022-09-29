using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess.NHibernate
{
    public abstract class NHinernateHelper : IDisposable
    {
        private static ISessionFactory _sessionFactory;//
        public virtual ISessionFactory SessionFactory
        {
            get { return _sessionFactory ?? (_sessionFactory = InitializationFactory()); } //session factory varsa döndür yoksa sf initialize et
        }
        protected abstract ISessionFactory InitializationFactory(); //oracle veya sql veritabanı göndericez
        public virtual ISession OpenSession()
        {
            return SessionFactory.OpenSession();   
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
