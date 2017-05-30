using System.Threading.Tasks;
using Domain.Infrastructure.Events;
using MassTransit;

namespace Domain.Infrastructure.Messaging
{
    public class MassTransitMessageHandlerDecorator<TMessage> : IMessageHandler<TMessage>, IDomainEvent, IConsumer<TMessage>
        where TMessage : class, IMessage
    {
        private readonly IMessageHandler<TMessage> _messageHandler;

        public MassTransitMessageHandlerDecorator(IMessageHandler<TMessage> messageHandler)
        {
            _messageHandler = messageHandler;
        }

        public void Handle(TMessage @event)
        {
            _messageHandler.Handle(@event);
        }

        public void Consume(TMessage message)
        {
            _messageHandler.Handle(message);
        }

        public Task Consume(ConsumeContext<TMessage> context)
        {
            _messageHandler.Handle(context.Message);

            return Task.FromResult(0);
        }
    }
}