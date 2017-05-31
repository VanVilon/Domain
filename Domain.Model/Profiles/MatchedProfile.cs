using System;
using System.Collections.Generic;
using System.Text;
using Domain.Infrastructure;

namespace ProfilesMatcher.Domain.Model.Profiles
{
    public class MatchedProfile : AggregateRoot
    {
        public Guid ProfileId { get; }
        public IEnumerable<Guid> MatchingProfiles { get; }

        public MatchedProfile(Guid profileId, IEnumerable<Guid> matchingProfiles)
        {
            ProfileId = profileId;
            MatchingProfiles = matchingProfiles;
        }
    }
}
