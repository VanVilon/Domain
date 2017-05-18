using System.Collections.Generic;

namespace Domain.Model.Events.Sourcing
{
    public interface IEventStore
    {
        void Append(IDomainEvent @event);
        List<StoredEvent> GetAllStoredEventsSince(uint eventId);
        List<StoredEvent> GetAllStoredEventsBetween(uint fromEventId, uint toEventId);
    }
}
