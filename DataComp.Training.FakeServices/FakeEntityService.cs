using Bogus;
using DataComp.Training.IServices;
using DataComp.Training.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataComp.Training.FakeServices
{

    public class FakeEntityServiceOptions
    {
        public int Count { get; set; }
    }

    public abstract class FakeEntityService<TEntity, TSearchCriteria> : IEntityService<TEntity, TSearchCriteria>
    where TEntity : BaseEntity
    {
        protected readonly ICollection<TEntity> entities;


        // dotnet add package Microsoft.Extensions.Options
        public FakeEntityService(Faker<TEntity> faker, IOptions<FakeEntityServiceOptions> options)
        {
            entities = faker.Generate(options.Value.Count);
        }

        public virtual void Add(TEntity entity)
        {
            entity.Id = Guid.NewGuid();

            entities.Add(entity);
        }

        public virtual ICollection<TEntity> Get()
        {
            return entities;
        }

        public virtual TEntity Get(Guid id)
        {
            return entities.SingleOrDefault(e => e.Id == id);
        }

        public abstract ICollection<TEntity> Get(TSearchCriteria searchCriteria);

        public virtual void Remove(Guid id)
        {
            entities.Remove(Get(id));
        }

        public virtual void Update(TEntity entity)
        {
            Remove(entity.Id);
            Add(entity);
        }
    }
}
