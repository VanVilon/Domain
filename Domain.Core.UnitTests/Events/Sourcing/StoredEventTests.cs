using System;
using System.Collections.Generic;
using System.Text;
using Domain.Model.Events;
using Domain.Model.Events.Sourcing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Domain.Model.UnitTests.Events.Sourcing
{
    [TestClass]
    public class StoredEventTests
    {
        [TestMethod]
        public void Can_convert_stored_event_to_domain_event()
        {
            var eventBody = JsonConvert.SerializeObject(new TestEvent() {SomeimportantProperty = "lol"});
            var storedEvent = new StoredEvent(typeof(TestEvent), new EventBody(eventBody));

            var domainEvent = (TestEvent) storedEvent.ToDomainEvent();
            Assert.IsInstanceOfType(domainEvent, typeof(TestEvent));
            Assert.AreEqual("lol", domainEvent.SomeimportantProperty);
        }

        [TestMethod]
        public void Stored_event_is_equatable_to_other_stored_event_with_same_data()
        {
            var se1 = new StoredEvent(typeof(TestEvent), new EventBody("a"));
            var se2 = new StoredEvent(typeof(TestEvent), new EventBody("a"));

            Assert.AreEqual(se1, se2);
        }

        [TestMethod]
        public void Stored_event_is_not_equatable_to_other_stored_event_with_different_data()
        {
            var se1 = new StoredEvent(typeof(TestEvent), new EventBody("a"));
            var se2 = new StoredEvent(typeof(TestEvent), new EventBody("b"));

            Assert.AreNotEqual(se1, se2);
        }
    }

    public class TestEvent : IDomainEvent
    {
        public int Version { get; set; }
        public DateTime OccuredDate { get; set; }

        public string SomeimportantProperty { get; set; }
    }
}
