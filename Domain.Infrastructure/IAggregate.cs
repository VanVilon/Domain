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
        uint Version { get; }
        IEnumerable<IDomainEvent> UncommitedEvents { get; }
        
        void Apply(IDomainEvent @event);
        void ClearUncommitedEvents();
        void LoadFromHistory(List<IDomainEvent> events);
    }
}
