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
        protected readonly UniqueQueue<IMongoRepositoryChange> Changes = new UniqueQueue<IMongoRepositoryChange>();

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
            Changes.Enqueue(new InsertedAggregate(entity));
        }

        public void Remove(TEntity entity)
        {
            Changes.Enqueue(new DeletedAggregate(entity));
        }

        public void Update(TEntity entity)
        {
            //TODO add to changes
        }

        public void SaveChanges()
        {
            //TODO Commit changes to mongodb server
        }
    }

    public interface IMongoRepositoryChange
    {
        void Apply(IMongoCollection<IAggregate> collection);
    }

    public class DeletedAggregate : IMongoRepositoryChange, IEquatable<DeletedAggregate>
    {
        private readonly Guid _aggregateId;

        public DeletedAggregate(Guid aggregateId)
        {
            _aggregateId = aggregateId;
        }

        public void Apply(IMongoCollection<IAggregate> collection)
        {
            collection.DeleteOne(aggregate => aggregate.Id == _aggregateId);
        }

        public bool Equals(DeletedAggregate other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other._aggregateId == this._aggregateId;
        }

        public override bool Equals(object obj)
        {
            return Equals((DeletedAggregate) obj);
        }

        public override int GetHashCode()
        {
            return _aggregateId.GetHashCode();
        }
    }

    public class InsertedAggregate : IMongoRepositoryChange
    {
        private readonly IAggregate _aggregate;

        public InsertedAggregate(IAggregate aggregate)
        {
            _aggregate = aggregate;
        }

        public void Apply(IMongoCollection<IAggregate> collection)
        {
            collection.InsertOne(_aggregate);
            //TODO investigate BsonClassMap
        }
    }
}
