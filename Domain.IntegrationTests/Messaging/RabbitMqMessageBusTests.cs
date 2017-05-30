using System;
using System.IO;
using System.Threading.Tasks;
using Domain.Infrastructure;
using Domain.Infrastructure.Events;
using Domain.Infrastructure.Messaging;
using Domain.Messaging.RabbitMq;
using Domain.Messaging.RabbitMq.Infrastructure;
using MassTransit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.IntegrationTests.Messaging
{
    [TestClass]
    public class RabbitMqMessageBusTests
    {
        private RabbitMqMassTransitDomainEventBus _bus;

        [TestInitialize]
        public void Initialize()
        {
            _bus = new RabbitMqMassTransitDomainEventBus(s =>
            {
                s.Handler<TestEvent>(context =>
                {
                    return File.WriteAllTextAsync("C:\\Users\\Magorion\\Desktop\\test.txt", "TEST");
                });
            });
        }

        [TestMethod]
        public void Publish_sends_event_through_rabbitMq_queue()
        {
            _bus.Publish(new TestEvent());
        }

        [TestMethod]
        public async Task PublishAsync_sends_event_asynchronously_through_rabbitMq_queue()
        {
        }
    }

    public class TestEvent : IDomainEvent
    {
        public string ImportantData { get; set; }
    }

    public class TestEventHandler : IMessageHandler<TestEvent>
    {
        public void Handle(TestEvent message)
        {
            File.WriteAllText("C:\\Users\\Magorion\\Desktop\\test.txt", "TEST");
        }
    }
}
