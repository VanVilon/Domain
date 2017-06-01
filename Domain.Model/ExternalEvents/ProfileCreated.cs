using System;
using Domain.Infrastructure.Events;

namespace ProfilesMatcher.Domain.Model.ExternalEvents
{
    public class ProfileCreated : IDomainEvent
    {
        public Guid ProfileId { get; set; }
    }
}