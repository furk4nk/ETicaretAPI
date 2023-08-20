using ETicaretAPI.Domain.Common;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Services.Repositories
{
    public interface IReadRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predice, bool tracking = false,
                               Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> Include = null);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null, bool tracking = false,
                                   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                   Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> Include = null);
        TEntity GetById(string id, bool tracking = false);
    }
}
