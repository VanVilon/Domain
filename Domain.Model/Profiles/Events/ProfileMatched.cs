using System;
using System.Collections.Generic;
using Domain.Infrastructure.Events;

namespace ProfilesMatcherContext.Domain.Model.Profiles.Events
{
    public class ProfileMatched : IDomainEvent
    {
        public string ProfileId { get; }
        public IEnumerable<string> MatchingProfileIds { get; }

        public ProfileMatched(string profileId, IEnumerable<string> matchingProfileIds)
        {
            ProfileId = profileId;
            MatchingProfileIds = matchingProfileIds;
        }
    }
}