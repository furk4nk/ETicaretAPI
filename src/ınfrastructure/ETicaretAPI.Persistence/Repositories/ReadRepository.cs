using ETicaretAPI.Application.Services.Repositories;
using ETicaretAPI.Domain.Common;
using ETicaretAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories
{
    public class ReadRepository<TEntity,TContext> : IReadRepository<TEntity>
        where TEntity : BaseEntity
        where TContext : DbContext
    {
        protected TContext _context;

        public ReadRepository(TContext context)
        {
            _context=context;
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null, bool tracking = false,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> Include = null)
        {
            var query = Query();
            if (predicate is not null) query = query.Where(predicate);
            if (orderBy != null) query = orderBy(query);
            if (Include != null) query = Include(query);
            if (tracking) query = query.AsNoTracking();

            return query;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predice, bool tracking = false,
                                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> Include = null)
        {
            IQueryable<TEntity> query = Query();
            if (Include != null) query = Include(query);
            if (tracking) query = query.AsNoTracking();

            TEntity entity = await query.FirstOrDefaultAsync(predice);
            return entity;
        }

        public TEntity GetById(string id, bool tracking = false)
        {
            IQueryable<TEntity> query = Query();
            if (tracking)
                query = query.AsNoTracking();
            return query.FirstOrDefault(x => x.Id == Guid.Parse(id));
        }

        public IQueryable<TEntity> Query()
        {
            return _context.Set<TEntity>();
        }
    }
}
