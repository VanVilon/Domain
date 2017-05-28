using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Infrastructure;
using Domain.Infrastructure.Helpers;
using MongoDB.Driver;

namespace Domain.Persistence.Providers.MongoDb
{
    public abstract class MongoDbRepository<TEntity> : IRepository<TEntity> where TEntity: IAggregate
    {
        private readonly IMongoCollection<TEntity> _mongoCollection;

        protected MongoDbRepository(MongoDataContext mongoDataContext)
        {
            var entityName = typeof(TEntity).Name;
            _mongoCollection = mongoDataContext.GetDatabase().GetCollection<TEntity>(entityName);
        }

        public TEntity FindById(Guid id)
        {
            return FindByIdAsync(id).Result;
        }

        public async Task<TEntity> FindByIdAsync(Guid id)
        {
            return await _mongoCollection.Find(entity => entity.Id == id).SingleAsync();
        }

        public TEntity FindOne(Expression<Func<TEntity, bool>> predicate)
        {
            return _mongoCollection.AsQueryable().FirstOrDefault(predicate);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _mongoCollection.AsQueryable().Where(predicate);
        }

        public void Add(TEntity entity)
        {
            AddAsync(entity).Wait();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _mongoCollection.InsertOneAsync(entity);
        }

        public void Remove(TEntity entity)
        {
            RemoveAsync(entity).Wait();
        }

        public async Task RemoveAsync(TEntity entity)
        {
            await _mongoCollection.DeleteOneAsync(e => e.Id == entity.Id);
        }

        public void Update(TEntity entity)
        {
            UpdateAsync(entity).Wait();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await _mongoCollection.UpdateOneAsync(e => e.Id == entity.Id, new ObjectUpdateDefinition<TEntity>(entity));
        }
    }
}
