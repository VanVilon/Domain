using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Core
{
    public abstract class EntityWithCompositeId : Entity, IEquatable<EntityWithCompositeId>
    {
        public abstract IEnumerable<object> GetIdentityComponents();

        public bool Equals(EntityWithCompositeId other)
        {
            return GetIdentityComponents().SequenceEqual(other.GetIdentityComponents());
        }

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(null, obj))
                return false;
            if (Object.ReferenceEquals(this, obj))
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