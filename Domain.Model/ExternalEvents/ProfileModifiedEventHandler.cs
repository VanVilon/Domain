using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Infrastructure;
using Domain.Infrastructure.Messaging;
using ProfilesMatcher.Domain.Model.MatchedProfiles.Events;

namespace ProfilesMatcher.Domain.Model.ExternalEvents
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
            (Guid profileId, Guid[] matchingProfiles) = await _matcher.RematchProfileAsync(message.ProfileId);
            await _domainEventBus.Publish(new ProfileMatched(profileId, matchingProfiles));
        }
    }

    public interface IProfilesMatcher
    {
        Task<(Guid, Guid[])> RematchProfileAsync(Guid profileId);
    }
}
