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
        private readonly MongoDataContext _mongoDataContext;
        private readonly IMongoCollection<TEntity> _mongoCollection;

        protected MongoDbRepository(MongoDataContext mongoDataContext)
        {
            var entityName = typeof(TEntity).Name.ToLower();

            _mongoDataContext = mongoDataContext;
            _mongoCollection = _mongoDataContext.GetDatabase().GetCollection<TEntity>(entityName);
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
        }

        public void Remove(TEntity entity)
        {
        }

        public void Update(TEntity entity)
        {
            //TODO add to changes
        }
    }
}
