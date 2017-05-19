using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : IAggregate
    {
        TEntity FindById(Guid id);
        TEntity FindOne(Expression<Func<TEntity>> predicate);
        IEnumerable<TEntity> Find(Expression<Func<TEntity>> spec);
        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}
