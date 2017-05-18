using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Core.Events
{
    public interface IDomainEventBus
    {
        void Publish(IDomainEvent @event);
    }
}
