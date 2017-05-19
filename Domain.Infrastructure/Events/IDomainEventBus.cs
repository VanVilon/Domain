namespace Domain.Infrastructure.Events
{
    public interface IDomainEventBus
    {
        void Publish(IDomainEvent @event);
    }
}
