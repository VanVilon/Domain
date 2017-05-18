using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Core.Helpers;

namespace Domain.Core
{
    /// <summary>
    /// Overriden in a class, identifies this class as 'entity'.
    /// </summary>
    public abstract class Entity
    {
    }

    public abstract class EntityWithCompositeId : Entity, IEquatable<EntityWithCompositeId>
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