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
    }

    public class TestEvent : IDomainEvent
    {
        public int Version { get; set; }
        public DateTime OccuredDate { get; set; }

        public string SomeimportantProperty { get; set; }
    }
}
