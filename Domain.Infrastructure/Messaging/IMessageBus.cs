using System.Threading.Tasks;
using Domain.Infrastructure.Events;

namespace Domain.Infrastructure.Messaging
{
    public interface IMessageBus
    {
        void Publish(IDomainEvent @event);
        Task PublishAsync(IDomainEvent @event);
        void Send(ICommand command);
        Task SendAsync(ICommand command);
    }
}
