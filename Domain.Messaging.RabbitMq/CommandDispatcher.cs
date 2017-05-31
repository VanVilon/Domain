using System;
using System.Threading.Tasks;
using Domain.Infrastructure;
using Domain.Infrastructure.Messaging;
using RawRabbit;

namespace Domain.Messaging.RabbitMq
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IBusClient _bus;

        public CommandDispatcher(IBusClient bus)
        {
            _bus = bus;
        }

        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            await _bus.PublishAsync(command);
        }
    }
}
