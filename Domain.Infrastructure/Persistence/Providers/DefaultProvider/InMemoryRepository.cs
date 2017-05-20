using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Infrastructure.Persistence.Providers.DefaultProvider
{
    public abstract class InMemoryRepository<TEntity> : IRepository<TEntity> where TEntity: IAggregate
    {
        protected readonly HashSet<TEntity> Collection = new HashSet<TEntity>(new AggregateEqualityComparer<TEntity>());

        public TEntity FindById(Guid id)
        {
            return Collection.FirstOrDefault(entity => entity.Id == id);
        }

        public TEntity FindOne(Func<TEntity, bool> predicate)
        {
            return Collection.FirstOrDefault(predicate);
        }

        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            return Collection.Where(predicate);
        }

        public void Add(TEntity entity)
        {
            Collection.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            Collection.Remove(entity);
        }
    }

    internal class AggregateEqualityComparer<TEntity> : IEqualityComparer<TEntity> where TEntity: IAggregate
    {
        public bool Equals(TEntity x, TEntity y)
        {
            return x.GetType() == y.GetType() && x.Id == y.Id;
        }

        public int GetHashCode(TEntity obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
