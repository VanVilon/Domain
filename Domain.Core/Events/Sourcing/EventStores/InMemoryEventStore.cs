using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Domain.Model.Events.Sourcing.EventStores
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly ConcurrentBag<StoredEvent> _storedEvents = new ConcurrentBag<StoredEvent>();

        public InMemoryEventStore()
        {
            
        }

        public InMemoryEventStore(List<StoredEvent> store)
        {
            this._storedEvents = new ConcurrentBag<StoredEvent>(store);
        }

        public void Append(IDomainEvent @event)
        {
            var eventContent = JsonConvert.SerializeObject(@event);
            var eventBody = new EventBody(eventContent);
            var eventToStore = new StoredEvent(@event.GetType(), eventBody);
            _storedEvents.Add(eventToStore);
        }

        public List<StoredEvent> GetAllStoredEventsSince(uint eventId)
        {
            return _storedEvents.Where(@event => @event.ToDomainEvent().Version >= eventId).ToList();
        }

        public List<StoredEvent> GetAllStoredEventsBetween(uint fromEventId, uint toEventId)
        {
            return _storedEvents.Where(@event =>
            {
                var domainEvent = @event.ToDomainEvent();
                return domainEvent.Version >= fromEventId && domainEvent.Version <= toEventId;
            }).ToList();
        }
    }
}
