using System;
using System.Threading.Tasks;
using Domain.Infrastructure;
using Domain.Infrastructure.Messaging;
using ProfilesMatcherContext.Domain.Model.Profiles;
using ProfilesMatcherContext.Domain.Model.Profiles.Events;

namespace ProfilesMatcherContext.Domain.Model.ExternalEvents
{
    public class ProfileModifiedEventHandler : IEventHandler<ProfileModified>
    {
        private readonly IDomainEventBus _domainEventBus;
        private readonly IProfilesMatcher _matcher;

        public ProfileModifiedEventHandler(IDomainEventBus domainEventBus, IProfilesMatcher matcher)
        {
            _domainEventBus = domainEventBus;
            _matcher = matcher;
        }

        public async Task HandleAsync(ProfileModified message)
        {
            var matchedProfile = await _matcher.RematchProfileAsync(message.ProfileId);
            await _domainEventBus.Publish(new ProfileMatched(matchedProfile.ProfileId, matchedProfile.MatchingProfileIds));
        }
    }
}
