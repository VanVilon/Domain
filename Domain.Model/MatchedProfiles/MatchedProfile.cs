using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Infrastructure;
using ProfilesMatcher.Domain.Model.MatchedProfiles.Events;

namespace ProfilesMatcher.Domain.Model.MatchedProfiles
{
    public class MatchedProfile : AggregateRoot, IEventHandler<ProfileMatched>
    {
        public Guid ProfileId { get; }
        public IEnumerable<Guid> MatchingProfileIds { get; private set; }
        private readonly object _lock = new object();

        public MatchedProfile(Guid profileId, IEnumerable<Guid> matchingProfileIds)
        {
            ProfileId = profileId;
            MatchingProfileIds = matchingProfileIds;

            RegisterHandler<ProfileMatched>(async @event => await HandleAsync(@event));
        }

        public Task HandleAsync(ProfileMatched message)
        {
            if (message.ProfileId != this.ProfileId)
                return Task.CompletedTask;

            lock (_lock)
            {
                MatchingProfileIds = message.MatchingProfileIds;
                return Task.CompletedTask;
            }
        }
    }
}
