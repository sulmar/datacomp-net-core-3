﻿using Bogus;
using DataComp.Training.IServices;
using DataComp.Training.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataComp.Training.FakeServices
{
    public abstract class FakeEntityServiceAsync<TEntity, TSearchCriteria> : IEntityServiceAsync<TEntity, TSearchCriteria>
        where TEntity : BaseEntity
    {
        protected readonly ICollection<TEntity> entities;

        public FakeEntityServiceAsync(Faker<TEntity> faker)
        {
            entities = faker.Generate(100);
        }

        public Task AddAsync(TEntity entity)
        {
            entities.Add(entity);

            return Task.CompletedTask;
        }

        public Task<ICollection<TEntity>> GetAsync()
        {
            return Task.FromResult(entities);
        }

        public Task<TEntity> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<TEntity>> GetAsync(TSearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
