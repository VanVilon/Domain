using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Infrastructure.Events;

namespace Domain.Infrastructure
{
    public interface IAggregate
    {
        Guid Id { get; }
        int Version { get; }
        IEnumerable<IDomainEvent> UncommitedEvents { get; }

        void ApplyEvent(IDomainEvent @event);
        void ClearUncommitedEvents();
        void Recreate(List<IDomainEvent> events);
    }
}
