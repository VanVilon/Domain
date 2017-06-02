using System;
using Domain.Infrastructure.Events;

namespace ProfilesMatcherContext.Domain.Model.ExternalEvents
{
    public class ProfileCreated : IDomainEvent
    {
        public string ProfileId { get; set; }
    }
}