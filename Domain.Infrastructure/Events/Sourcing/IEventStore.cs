using System.Collections.Generic;

namespace Domain.Infrastructure.Events.Sourcing
{
    public interface IEventStore
    {
        void Append(IDomainEvent @event);
        List<IStoredEvent> GetAllStoredEventsSince(uint eventId);
        List<IStoredEvent> GetAllStoredEventsBetween(uint fromEventId, uint toEventId);
    }

    public interface IStoredEvent
    {
    }
}
