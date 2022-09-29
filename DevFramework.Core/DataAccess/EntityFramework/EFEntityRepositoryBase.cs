using DevFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess.EntityFramework
{
    public class EFEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public TEntity Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity=context.Entry(entity);//eklenecek değeri değişkene atıp
                addedEntity.State = EntityState.Added; //durumunu eklenecek olarak belirle 
                context.SaveChanges();
                return entity;//eklenmiş nesneyi döndürüyoruz
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);//silinecek değeri değişkene atıp
                deletedEntity.State = EntityState.Deleted; //durumunu silinecek olarak belirle 
                context.SaveChanges();
                //silinen değeri döndürmeye gerek yok
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);//tek bir nesne döndüreceğiz.SingleOrDefault ile dizi içinden tek birtane gelecek.
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter=null)//filterdan gelen değer yok ise,ilgili TEntity'e Set ile DbContext'ine abone oluyoruz,filtre doluysa where şartına filterı gönderip çekiyoruz.
        {
            using (var context=new TContext())
            {
                return filter==null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();    
            }
        }

        public TEntity Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);//değiştirilecek değeri değişkene atıp
                updatedEntity.State = EntityState.Modified; //durumunu değişecek olarak belirle 
                context.SaveChanges();
                return entity;//değişmiş nesneyi döndürüyoruz
            }
        }
    }
}
