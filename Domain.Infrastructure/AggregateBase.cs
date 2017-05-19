using System;
using System.Collections.Generic;
using Domain.Infrastructure.Events;
using Microsoft.CSharp.RuntimeBinder;

namespace Domain.Infrastructure
{
    public abstract class AggregateBase : IAggregate
    {
        private Dictionary<Type, Action<IDomainEvent>> _routes;
        private readonly List<IDomainEvent> _uncommitedEvents;
        public Guid Id { get; }
        public int Version { get; protected set; }

        public IEnumerable<IDomainEvent> UncommitedEvents => _uncommitedEvents;

        protected AggregateBase()
        {
            _uncommitedEvents = new List<IDomainEvent>();
        }

        public void ApplyEvent(IDomainEvent @event)
        {
            try
            {
                ((dynamic)this).Apply(@event);
                Version++;
            }
            catch (RuntimeBinderException)
            {
            }
        }

        public void ClearUncommitedEvents()
        {
            _uncommitedEvents.Clear();
        }

        public void Recreate(List<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                ApplyEvent(@event);
            }
        }
    }
}