using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Model.Events.Sourcing;
using Domain.Model.Events.Sourcing.EventStores;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Model.UnitTests.Events.Sourcing.EventStores
{
    [TestClass]
    public class InMemoryEventStoreTests
    {
        [TestMethod]
        public void Events_are_presisted_in_event_store()
        {
            var initialStore = new List<StoredEvent>();
            var eventStore = new InMemoryEventStore(initialStore);

            var domainEvent = new TestEvent {SomeimportantProperty = "a", OccuredDate = DateTime.Now};
            eventStore.Append(domainEvent);
            eventStore.GetAllStoredEventsSince(0); //TODO Handle eventId in IDomainEvent
        }
    }
}
