using Domain.Persistence.Providers.MongoDb;

namespace ProfilesMatcherContext.Domain.Model.Profiles.Repositories
{
    public class MatchedProfilesRepository : MongoDbRepository<MatchedProfile>
    {
        public MatchedProfilesRepository(MongoDataContext mongoDataContext) 
            : base(mongoDataContext)
        {
        }
    }
}
