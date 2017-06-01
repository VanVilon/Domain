using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProfilesMatcher.Domain.Model.MatchedProfiles;
using ProfilesMatcher.Domain.Model.MatchedProfiles.Events;

namespace ProfilesMatcher.Service.Controllers
{
    [Route("api/matchedProfiles")]
    public class MatchedProfilesController : Controller
    {
        // GET api/matchedProfiles
        [HttpGet]
        public IEnumerable<MatchedProfile> Get()
        {
            return new List<MatchedProfile>
            {
                new MatchedProfile(Guid.NewGuid(), new[] {Guid.NewGuid(), Guid.NewGuid() }),
                new MatchedProfile(Guid.NewGuid(), new[] {Guid.NewGuid(), Guid.NewGuid() }),
                new MatchedProfile(Guid.NewGuid(), new[] {Guid.NewGuid(), Guid.NewGuid() })
            };
        }

        // GET api/matchedProfiles/:id
        [HttpGet]
        [Route("{id}")]
        public MatchedProfile Get(Guid id)
        {
            var matchedProfile = new MatchedProfile(Guid.NewGuid(), new[] {Guid.NewGuid(), Guid.NewGuid()});
            matchedProfile.Apply(new ProfileMatched(matchedProfile.ProfileId, new List<Guid>{Guid.Empty}));
            return matchedProfile;
        }
    }
}
