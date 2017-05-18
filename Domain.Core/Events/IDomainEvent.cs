using System;

namespace Domain.Model.Events
{
    /// <summary>
    /// Overriden in a class, identifies this class as domain event.
    /// </summary>
    public interface IDomainEvent
    {
        int Version { get; }
        DateTime OccuredDate { get; }
    }
}