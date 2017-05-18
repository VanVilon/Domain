using System;
using System.Collections.Generic;
using System.Text;
using Domain.Model.Events;
using Domain.Model.Events.Sourcing;
using Newtonsoft.Json;
using Xunit;

namespace Domain.Model.UnitTests.Events.Sourcing
{
    public class StoredEventTests
    {
        [Fact]
        public void Can_convert_stored_event_to_domain_event()
        {
            var eventBody = JsonConvert.SerializeObject(new TestEvent() {SomeimportantProperty = "lol"});
            var storedEvent = new StoredEvent(typeof(TestEvent), new EventBody(eventBody));

            var domainEvent = (TestEvent) storedEvent.ToDomainEvent();
            Assert.IsType<TestEvent>(domainEvent);
            Assert.Equal("lol", domainEvent.SomeimportantProperty);
        }
    }

    public class TestEvent : IDomainEvent
    {
        public int Version { get; set; }
        public DateTime OccuredDate { get; set; }

        public string SomeimportantProperty { get; set; }
    }
}
