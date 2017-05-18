using System;

namespace Domain.Model.Events
{
    public interface IDomainEvent
    {
        int Version { get; set; }
        DateTime OccuredDate { get; set; }
    }
}