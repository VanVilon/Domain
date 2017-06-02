using System;
using System.Linq;
using ProfilesMatcherContext.Domain.Model.Profiles;

namespace ProfilesMatcherContext.Service.DataTransferObjects
{
    public static class Dtos
    {
        public static MatchedProfileDto ToDto(MatchedProfile matchedProfile)
        {
            return new MatchedProfileDto(matchedProfile.ProfileId, matchedProfile.MatchingProfileIds.ToArray());
        }
    }

    public class MatchedProfileDto
    {
        public string Id { get; }
        public string[] MatchingProfileIds { get; }

        public MatchedProfileDto(string id, string[] matchingProfileIds)
        {
            Id = id;
            MatchingProfileIds = matchingProfileIds;
        }
    }
}
