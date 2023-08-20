using ETicaretAPI.Application.Services.Repositories;
using ETicaretAPI.Domain.Common;
using ETicaretAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Persistence.Repositories
{
    public class WriteRepository<TEntity, TContext> : IWriteRepository<TEntity>
        where TEntity : BaseEntity
        where TContext : DbContext
    {
        protected TContext _context;

        public WriteRepository(TContext context)
        {
            _context=context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> AddRangeAsync(List<TEntity> entities)
        {
            entities.ForEach(entity => _context.Entry(entity).State = EntityState.Added);
            return await _context.SaveChangesAsync() is not 0;
        }

        public IQueryable<TEntity> Query()
        {
            return _context.Set<TEntity>();
        }

        public TEntity Remove(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
            return entity;
        }

        public bool RemoveRange(List<TEntity> entities)
        {
            entities.ForEach(entity => _context.Entry(entity).State = EntityState.Deleted);
            return _context.SaveChanges() is not 0;
        }
        public TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public bool UpdateRange(List<TEntity> entities)
        {
            entities.ForEach(entity => _context.Entry(entity).State = EntityState.Modified);
            return _context.SaveChanges() is not 0;
        }
    }
}
