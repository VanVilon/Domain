using System;
using System.Collections.Generic;
using Domain.Infrastructure.Events;

namespace ProfilesMatcher.Domain.Model.MatchedProfiles.Events
{
    public class ProfileMatched : IDomainEvent
    {
        public Guid ProfileId { get; }
        public IEnumerable<Guid> MatchingProfileIds { get; }

        public ProfileMatched(Guid profileId, IEnumerable<Guid> matchingProfileIds)
        {
            ProfileId = profileId;
            MatchingProfileIds = matchingProfileIds;
        }
    }
}