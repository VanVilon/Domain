using System.Threading.Tasks;
using Domain.Infrastructure;

namespace ProfilesMatcher.Domain.Model.ExternalEvents
{
    public class ProfileCreatedEventHandler : IEventHandler<ProfileCreated>
    {
        public ProfileCreatedEventHandler(IProfilesMatcher matcher)
        {
            
        }

        public async Task HandleAsync(ProfileCreated message)
        {
            await _matcher.CreateProfileAsync(message.ProfileId);
        }
    }
}
