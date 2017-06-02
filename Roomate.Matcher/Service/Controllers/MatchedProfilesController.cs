using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using ProfilesMatcherContext.Domain.Model.Profiles;
using ProfilesMatcherContext.Service.DataTransferObjects;

namespace ProfilesMatcherContext.Service.Controllers
{
    [Route("api/matchedProfiles")]
    public class MatchedProfilesController : Controller
    {
        private readonly IProfilesMatcher _matcher;

        public MatchedProfilesController(IProfilesMatcher matcher)
        {
            _matcher = matcher;
        }

        // GET api/matchedProfiles/:id
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(string id)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                MatchedProfile matchedProfile = _matcher.GetMatchedProfile(id);
                return new JsonResult(Dtos.ToDto(matchedProfile));
            }
            catch (Exception)
            {
                //Log
                throw;
            }
        }
    }
}
