using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UdemyNLayerProject.Core.Services
{
    public interface IService<TEntity> where TEntity : class
    {

        /// <summary>
        /// Direkt IRepository ile neden haberlesmiyorum da içerisinde yer alan tüm methodları buraya kopyaladım? İleride SQL Server yerine Oracle plsql alt yapısını kullanmak istediğimde, IService sabit kalacak, Repositories klasöründe IOracleRepository'lerimi olusturmam yeterli olacak.
        /// </summary>

        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        //herhangi parametreye göre ilgili nesneleri bul
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        //herhangi bir nesnesine göre getir
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        TEntity Update(TEntity entity);
    }
}
