using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Persistence.Providers.MongoDb;
using ProfilesMatcherContext.Domain.Model.Profiles.Repositories;

namespace ProfilesMatcherContext.Domain.Model.Profiles
{
    public class ProfilesMatcher : IProfilesMatcher
    {
        private readonly ProfilesAdapter _profilesAdapter;
        private readonly MongoDbRepository<MatchedProfile> _matchedProfilesRepository;

        public ProfilesMatcher(ProfilesAdapter profilesAdapter, MongoDbRepository<MatchedProfile> matchedProfilesRepository)
        {
            _profilesAdapter = profilesAdapter;
            _matchedProfilesRepository = matchedProfilesRepository;
        }

        public async Task<MatchedProfile> RematchProfileAsync(string profileId)
        {
            var result = _matchedProfilesRepository.FindOne(profile => profile.ProfileId == profileId);
            if (result == null)
                throw new Exception($"Profile with id {profileId} was not yet matched and cannot be re-matched");

            return await MatchProfileAsync(profileId);
        }

        public async Task<MatchedProfile> MatchProfileAsync(string profileId)
        {
            var allProfiles = await _profilesAdapter.GetAsync<Profile[]>("api/profiles");
            var results = new List<string>();

            if (allProfiles != null)
            {
                var profileToMatch = allProfiles.FirstOrDefault(profile => profile.Id == profileId);

                if (profileToMatch == null)
                    throw new Exception($"Profile with id {profileId} does not exist");

                var toCheck = allProfiles.Except(new[] { profileToMatch });

                foreach (var profile in toCheck)
                {
                    if (profile.AcceptsProfile(profileToMatch))
                        results.Add(profile.Id);
                }
            }

            var matchedProfile = new MatchedProfile(profileId, results);
            _matchedProfilesRepository.Add(matchedProfile);

            return matchedProfile;
        }

        public MatchedProfile GetMatchedProfile(string profileId)
        {
            var result = _matchedProfilesRepository.FindOne(profile => profile.ProfileId == profileId);

            if (result == null)
                return MatchProfileAsync(profileId).Result;

            return result;
        }
    }

    public class Profile
    {
        public string Id { get; set; }
        public Accepts Accepts { get; set; }

        public bool AcceptsProfile(Profile profileToMatch)
        {
            return profileToMatch.Accepts.Equals(this.Accepts);
        }
    }

    public class Accepts : IEquatable<Accepts>
    {
        public bool Drinking { get; set; }
        public bool Smoking { get; set; }
        public bool CleaningSchedule { get; set; }
        public bool Animals { get; set; }

        public bool Equals(Accepts other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Drinking == other.Drinking && Smoking == other.Smoking &&
                   CleaningSchedule == other.CleaningSchedule && Animals == other.Animals;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Accepts) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Drinking.GetHashCode();
                hashCode = (hashCode * 397) ^ Smoking.GetHashCode();
                hashCode = (hashCode * 397) ^ CleaningSchedule.GetHashCode();
                hashCode = (hashCode * 397) ^ Animals.GetHashCode();
                return hashCode;
            }
        }
    }
}