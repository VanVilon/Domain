using System.Threading.Tasks;
using Domain.Infrastructure;
using ProfilesMatcherContext.Domain.Model.Profiles;

namespace ProfilesMatcherContext.Domain.Model.ExternalEvents
{
    public class ProfileCreatedEventHandler : IEventHandler<ProfileCreated>
    {
        private readonly IProfilesMatcher _matcher;

        public ProfileCreatedEventHandler(IProfilesMatcher matcher)
        {
            _matcher = matcher;
        }

        public async Task HandleAsync(ProfileCreated message)
        {
            await _matcher.MatchProfileAsync(message.ProfileId);
        }
    }
}
