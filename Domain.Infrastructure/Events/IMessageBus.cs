using System.Threading.Tasks;

namespace Domain.Infrastructure.Events
{
    public interface IMessageBus
    {
        void Publish(IDomainEvent @event);
        Task PublishAsync(IDomainEvent @event);
        void Send(ICommand command);
        Task SendAsync(ICommand command);
    }
}
