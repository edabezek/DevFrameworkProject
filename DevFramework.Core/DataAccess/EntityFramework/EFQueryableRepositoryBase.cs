using DevFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess.EntityFramework
{
    public class EFQueryableRepositoryBase<T> : IQueryableRepository<T> where T : class, IEntity, new()
    {
        private DbContext _context;
        private IDbSet<T> _entities;
        public EFQueryableRepositoryBase(DbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Table => this.Entities; //tablo çağırıldığında entities döndürecek

        protected virtual IDbSet<T> Entities //Baska sınıflar EFQueryableRepositoryBase'i inherit ederken kullanamasın diye protected yapıyoruz.
        {
            get 
            {
                if (_entities==null)
                {
                    _entities=_context.Set<T>();  //contexteki gelen t ye abone ol  
                }
                return _entities;
            }
        } 
    }
}
