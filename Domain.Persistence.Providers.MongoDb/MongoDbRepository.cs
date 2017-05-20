using System;
using System.Collections.Generic;
using Domain.Infrastructure;

namespace Domain.Persistence.Providers.MongoDb
{
    public class MongoDbRepository<TEntity> : IRepository<TEntity> where TEntity: IAggregate
    {
        public TEntity FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public TEntity FindOne(Func<TEntity, bool> predicate)
        {
            throw new NotImplementedException();
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
    }
}
