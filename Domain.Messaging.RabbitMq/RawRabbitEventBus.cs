using System.Threading.Tasks;
using Domain.Infrastructure.Events;
using Domain.Infrastructure.Messaging;
using RawRabbit.vNext.Disposable;

namespace Domain.Messaging.RabbitMq
{
    public class RawRabbitEventBus : IDomainEventBus
    {
        private readonly IBusClient _client;

        public RawRabbitEventBus(IBusClient client)
        {
            _client = client;
        }

        public async Task Publish(IDomainEvent @event)
        {
            await _client.PublishAsync(@event);
        }
    }
}