using ETicaretAPI.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Services.Repositories
{
    public interface IWriteRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity 
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<bool> AddRangeAsync(List<TEntity> entities);// goto 
        TEntity Remove(TEntity entity);
        bool RemoveRange(List<TEntity> entities);
        TEntity Update(TEntity entity);
        bool UpdateRange(List<TEntity> entities);
    }
}
        