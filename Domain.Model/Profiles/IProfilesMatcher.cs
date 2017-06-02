using System;
using System.Threading.Tasks;

namespace ProfilesMatcherContext.Domain.Model.Profiles
{
    public interface IProfilesMatcher
    {
        Task<MatchedProfile> RematchProfileAsync(string profileId);
        Task<MatchedProfile> MatchProfileAsync(string profileId);
        MatchedProfile GetMatchedProfile(string profileId);
    }
}