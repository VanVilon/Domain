using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Infrastructure.Events;

namespace Domain.Infrastructure.Messaging
{
    public interface IDomainEventBus
    {
        Task Publish(IDomainEvent @event);
    }
}
