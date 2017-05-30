using System.Reflection;
using Domain.Infrastructure;
using Domain.Infrastructure.Events;
using RawRabbit;
using RawRabbit.Common;

namespace Domain.Messaging.RabbitMq
{
    public static class RawRabbitExtensions
    {
        public static ISubscription SubscribeToCommand<TCommand>(this IBusClient bus,
            ICommandHandler<TCommand> handler, string name = null) where TCommand : ICommand
        {
            return bus.SubscribeAsync<TCommand>(async (msg, context) => await handler.HandleAsync(msg),
                cfg => cfg.WithQueue(q => q.WithName(GetExchangeName<TCommand>(name))));
        }

        public static ISubscription SubscribeToEvent<TEvent>(this IBusClient bus,
            IEventHandler<TEvent> handler, string name = null) where TEvent : IDomainEvent
        {
            return bus.SubscribeAsync<TEvent>(async (msg, context) => await handler.HandleAsync(msg),
                cfg => cfg.WithQueue(q => q.WithName(GetExchangeName<TEvent>(name))));
        }

        private static string GetExchangeName<T>(string name = null)
        {
            return string.IsNullOrWhiteSpace(name)
                ? $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}"
                : $"{name}/{typeof(T).Name}";
        }
    }
}