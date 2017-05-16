using System;

namespace Domain.Core.Events
{
    public interface IDomainEvent
    {
        int Version { get; set; }
        DateTime OccuredDate { get; set; }
    }
}