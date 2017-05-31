using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProfilesMatcher.Domain.Model.Profiles;

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
            return new MatchedProfile(Guid.NewGuid(), new[] {Guid.NewGuid(), Guid.NewGuid()});
        }
    }
}
