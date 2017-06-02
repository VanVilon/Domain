using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Infrastructure;
using ProfilesMatcherContext.Domain.Model.Profiles.Events;

namespace ProfilesMatcherContext.Domain.Model.Profiles
{
    public class MatchedProfile : AggregateRoot, IEventHandler<ProfileMatched>
    {
        public string ProfileId { get; private set; }
        public IEnumerable<string> MatchingProfileIds { get; private set; }
        private readonly object _lock = new object();

        public MatchedProfile(string id, IEnumerable<string> matchingProfileIds)
        {
            ProfileId = id;
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
