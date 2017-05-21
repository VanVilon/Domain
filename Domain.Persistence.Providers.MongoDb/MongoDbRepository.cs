using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Infrastructure;
using MongoDB.Driver;

namespace Domain.Persistence.Providers.MongoDb
{
    public class MongoDbRepository<TEntity> : IRepository<TEntity> where TEntity: IAggregate
    {
        private readonly MongoDataContext _mongoDataContext;

        public MongoDbRepository(MongoDataContext mongoDataContext)
        {
            _mongoDataContext = mongoDataContext;
        }

        public TEntity FindById(Guid id)
        {
            return this.FindByIdAsync(id).Result;
        }

        public async Task<TEntity> FindByIdAsync(Guid id)
        {
            var entityName = typeof(TEntity).Name.ToLower();

            var mongoCollection = _mongoDataContext.GetDatabase().GetCollection<TEntity>(entityName);
            return await mongoCollection.Find(entity => entity.Id == id).FirstOrDefaultAsync();
        }

        public TEntity FindOne(Func<TEntity, bool> predicate)
        {
            return this.FindOneAsync(predicate).Result;
        }

        public async Task<TEntity> FindOneAsync(Func<TEntity, bool> predicate)
        {
            var entityName = typeof(TEntity).Name.ToLower();

            var mongoCollection = _mongoDataContext.GetDatabase().GetCollection<TEntity>(entityName);
            return await mongoCollection.Find(entity => predicate(entity)).FirstOrDefaultAsync();
        }

        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
