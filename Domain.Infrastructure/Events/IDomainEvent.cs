using System;
using Domain.Infrastructure.Messaging;

namespace Domain.Infrastructure.Events
{
    /// <summary>
    /// Overriden in a class, identifies this class as domain event.
    /// </summary>
    public interface IDomainEvent : IMessage
    {
    }
}