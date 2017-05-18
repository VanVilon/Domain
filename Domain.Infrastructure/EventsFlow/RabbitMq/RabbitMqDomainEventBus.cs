using System;
using System.Collections.Generic;
using System.Text;
using Domain.Core.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Domain.Infrastructure.EventsFlow.RabbitMq
{
    public class RabbitMqDomainEventBus : IDomainEventBus
    {
        public RabbitMqDomainEventBus()
        {
        }

        public void Publish(IDomainEvent @event)
        {
            var connectionFactory = new ConnectionFactory() { Password = "a", UserName = "b", HostName = "localhost" };
            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();

            var serializedEWvent = JsonConvert.SerializeObject(new {Type = @event.GetType(), Body = @event});

            //TODO model.BasicPublish(serializedEvent);
        }
    }
}
