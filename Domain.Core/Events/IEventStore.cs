using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Core.Events
{
    public interface IEventStore
    {
        void Append(IDomainEvent @event);
        List<StoredEvent> GetAllStoredEventsSince(uint eventId);
        List<StoredEvent> GetAllStoredEventsBetween(uint fromEventId, uint toEventId);
    }

    public class StoredEvent
    {
        
    }
}
