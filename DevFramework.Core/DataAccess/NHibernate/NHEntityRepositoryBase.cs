using DevFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;//Query

namespace DevFramework.Core.DataAccess.NHibernate
{
    public class NHEntityRepositoryBase<TEntity> : IEntityRepository<TEntity> 
        where TEntity : class,IEntity,new()
    {
        private NHinernateHelper _nHinernateHelper;

        public NHEntityRepositoryBase(NHinernateHelper nHinernateHelper)
        {
            _nHinernateHelper = nHinernateHelper;
        }

        public TEntity Add(TEntity entity)
        {
            using (var session=_nHinernateHelper.OpenSession())//gönderilen veritabanına göre,o veri tabanına uygun session açacak
            {
                session.Save(entity);
                return entity;
            }
        }

        public void Delete(TEntity entity)
        {
            using (var session = _nHinernateHelper.OpenSession())
            {
                session.Delete(entity);
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var session = _nHinernateHelper.OpenSession())
            {              
                return session.Query<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter=null)
        {
            using (var session = _nHinernateHelper.OpenSession())
            {
                return filter==null ? session.Query<TEntity>().ToList() : session.Query<TEntity>().Where(filter).ToList();    
            }
        }

        public TEntity Update(TEntity entity)
        {
            using (var session = _nHinernateHelper.OpenSession())
            {
                session.Update(entity);
                return entity;
            }
        }
    }
}
