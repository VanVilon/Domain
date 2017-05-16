using System;

namespace Domain.Core
{
    public interface IDomainEvent
    {
        int Version { get; set; }
        DateTime OccuredDate { get; set; }
    }
}