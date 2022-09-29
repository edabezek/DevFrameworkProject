using DevFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess.NHibernate
{
    public class NHQueryableRepository<T> : IQueryableRepository<T> where T : class, IEntity, new()
    {
        private NHinernateHelper _nHinernateHelper;
        private IQueryable<T> _entities;//tabloya abone olmamız gerek
        public NHQueryableRepository(NHinernateHelper nHinernateHelper)
        {
            _nHinernateHelper = nHinernateHelper;
        }

        public IQueryable<T> Table { get { return this.Entities; } }

        public virtual IQueryable<T> Entities
        {
            get
            {
                if (_entities == null)//entities null , o zaman entity için bir session aç(ama kapatmıyoruz-kapatana kadar kapanmaz),yoksa olanı döndür.
                {
                    _entities = _nHinernateHelper.OpenSession().Query<T>();
                }
                return _entities;
            }
        }
    }
}
