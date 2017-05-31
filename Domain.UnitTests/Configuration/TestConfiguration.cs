using Domain.Infrastructure.Configuration;

namespace Domain.UnitTests.Configuration
{
    public class TestConfiguration : IConfiguration
    {
        public string SomeSpec { get; set; }
    }
}