using System;
using Newtonsoft.Json;

namespace Domain.Model.Events.Sourcing
{
    public class StoredEvent : IValueObject<StoredEvent>
    {
        public Type Type { get; }
        public EventBody EventBody { get; }

        public StoredEvent(Type type, EventBody eventBody)
        {
            Type = type;
            EventBody = eventBody;
        }

        public IDomainEvent ToDomainEvent()
        {
            var domainEvent = JsonConvert.DeserializeObject(EventBody.Content);

            return (IDomainEvent) Convert.ChangeType(domainEvent, Type);
        }

        public bool Equals(StoredEvent other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Type == other.Type && Equals(EventBody, other.EventBody);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((StoredEvent) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Type != null ? Type.GetHashCode() : 0) * 397) ^
                       (EventBody != null ? EventBody.GetHashCode() : 0);
            }
        }
    }
}