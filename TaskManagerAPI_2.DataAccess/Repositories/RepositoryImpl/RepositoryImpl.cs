using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManagerAPI_2.Data;
using TaskManagerAPI_2.Models;
using TaskManagerAPI_2.Repository.IRepository;

namespace TaskManagerAPI_2.Repository.RepositoryImpl
{
    public class RepositoryImpl<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal TaskDbContext context;
        internal DbSet<TEntity> dbSet;
        public RepositoryImpl(TaskDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }


        public async Task CreateAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<TEntity> query = dbSet;


            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if(filter != null)
            {
                query = query.Where(filter);
            }
            
            
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<TEntity> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if(filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }


  


        public async Task RemoveAsync(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            dbSet.Update(entity);
        }
    }
}
