using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Repositories;

namespace UdemyNLayerProject.Data.Repositories
{
    /// <summary>
    /// Generic Repository Class
    /// Core katmanında olusturduğumuz classları burada implement ediyoruz.
    /// </summary>

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;              // database'e erişiyorum
            _dbSet = context.Set<TEntity>(); // tablolara erişiyorum
        }
        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);  // await kw islem bitene kadar bu satırda kalmayı sağlar.
        }
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }
        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }
        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate);
        }
        public TEntity Update(TEntity entity)
        {
            /// <summary>
            /// saveChange() methodunu nerde kullanırsam; DB'ye bu entitydeki değişikliği yansıtır. 
            /// cok sütunlu tablolar için ideal kullanım.
            /// entityde tek bir alan değiştirirsek, DB'de tüm alanı upd edecek sorgu gönderir(dezavantaj)
            /// </summary>   
            
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
