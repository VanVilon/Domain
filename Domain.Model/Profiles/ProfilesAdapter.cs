using System;
using System.Threading.Tasks;
using Domain.Infrastructure.PortAdapters;

namespace ProfilesMatcherContext.Domain.Model.Profiles
{
    public class ProfilesAdapter : RestAdapter
    {
        public ProfilesAdapter(Uri baseUri) 
            : base(baseUri)
        {
        }
    }
}