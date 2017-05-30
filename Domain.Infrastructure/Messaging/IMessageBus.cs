using System;
using System.Threading.Tasks;
using Domain.Infrastructure.Events;
using MassTransit;

namespace Domain.Infrastructure.Messaging
{
    public interface IMessageBus
    {
        void Publish(IDomainEvent @event);
        Task PublishAsync(IDomainEvent @event);
        void Send(ICommand command);
        Task SendAsync(ICommand command);
    }

    public abstract class MassTransitDomainEventBus : IMessageBus, IDisposable
    {
        private readonly IBusControl _bus;
        private bool _disposed;

        protected MassTransitDomainEventBus(IBusControl bus)
        {
            _bus = bus;
            _bus.Start();
        }

        public void Publish(IDomainEvent @event)
        {
            _bus.Publish(@event).Wait();
        }

        public async Task PublishAsync(IDomainEvent @event)
        {
            await _bus.Publish(@event);
        }

        public void Send(ICommand command)
        {
            _bus.Send(command).Wait();
        }

        public async Task SendAsync(ICommand command)
        {
            await _bus.Send(command);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _bus.Stop();
            }

            _disposed = true;
        }
    }
}
