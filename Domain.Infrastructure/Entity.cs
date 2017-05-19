using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Infrastructure.Helpers;

namespace Domain.Infrastructure
{
    /// <summary>
    /// Overriden in a class, identifies this class as 'entity'.
    /// </summary>
    public interface IEntity
    {
    }

    public abstract class EntityWithCompositeId : IEntity, IEquatable<EntityWithCompositeId>
    {
        public abstract IEnumerable<object> GetIdentityComponents();

        public bool Equals(EntityWithCompositeId other)
        {
            return GetIdentityComponents().SequenceEqual(other.GetIdentityComponents());
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
                return false;
            if (object.ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((EntityWithCompositeId)obj);
        }

        public override int GetHashCode()
        {
            return HashCodeHelper.CombineHashCodes(this.GetIdentityComponents());
        }
    }
}