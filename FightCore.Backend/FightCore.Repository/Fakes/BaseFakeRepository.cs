using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bartdebever.Patterns.Models;
using Bartdebever.Patterns.Repositories;
using Bogus;

namespace FightCore.Repositories.Fakes
{
    public abstract class BaseFakeRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        protected Faker<TEntity> Faker;
        private static Random _random = new Random();

        protected virtual int ListUpperBound => 20;

        protected virtual int ListLowerBound => 5;

        public abstract void GenerateFaker();

        public virtual TEntity Find(Expression<Func<TEntity, bool>> query)
        {
            return Faker.Generate();
        }

        public virtual Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> query)
        {
            return Task.FromResult(Faker.Generate());
        }

        public virtual IEnumerable<TEntity> FindRange(Expression<Func<TEntity, bool>> query)
        {
            return CreateList();
        }

        public virtual Task<List<TEntity>> FindRangeAsync(Expression<Func<TEntity, bool>> query)
        {
            return Task.FromResult(CreateList());
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return CreateList();
        }

        public virtual Task<List<TEntity>> GetAllAsync()
        {
            return Task.FromResult(CreateList());
        }

        public virtual TEntity GetById(TKey id)
        {
            return Faker.Generate();
        }

        public virtual Task<TEntity> GetByIdAsync(TKey id)
        {
            return Task.FromResult(Faker.Generate());
        }

        public virtual TEntity Add(TEntity entity)
        {
            return entity;
        }

        public virtual Task<TEntity> AddAsync(TEntity entity)
        {
            return Task.FromResult(entity);
        }

        public virtual TEntity Update(TEntity entity)
        {
            return entity;
        }

        public virtual void Remove(TEntity entity)
        {
            // No need to implement this.
        }

        private List<TEntity> CreateList()
        {
            return Faker.Generate(_random.Next(ListLowerBound, ListUpperBound));
        }
    }
}
