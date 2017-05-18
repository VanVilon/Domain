namespace Domain.Model.Events
{
    public interface IDomainEventBus
    {
        void Publish(IDomainEvent @event);
    }
}
