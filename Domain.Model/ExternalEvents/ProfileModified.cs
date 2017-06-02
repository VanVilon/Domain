using System;
using Domain.Infrastructure.Events;

namespace ProfilesMatcherContext.Domain.Model.ExternalEvents
{
    public class ProfileModified : IDomainEvent
    {
        public string ProfileId { get; set; }
    }
}
