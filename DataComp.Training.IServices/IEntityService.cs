using System;
using System.Collections.Generic;

namespace DataComp.Training.IServices
{
    public interface IEntityService<TEntity, TSearchCriteria>
    {
        ICollection<TEntity> Get();
        TEntity Get(Guid id);
        ICollection<TEntity> Get(TSearchCriteria searchCriteria);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(Guid id);
    }
}
