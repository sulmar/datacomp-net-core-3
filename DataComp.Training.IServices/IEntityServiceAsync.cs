using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataComp.Training.IServices
{
    public interface IEntityServiceAsync<TEntity, TSearchCriteria>
    {
        Task<ICollection<TEntity>> GetAsync();
        Task<TEntity> GetAsync(Guid id);
        Task<ICollection<TEntity>> GetAsync(TSearchCriteria searchCriteria);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(Guid id);
    }
}
