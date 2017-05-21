using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : IAggregate
    {
        TEntity FindById(Guid id);
        TEntity FindOne(Func<TEntity, bool> predicate);
        IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
    }
}
