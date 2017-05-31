using System.Collections.Generic;

namespace Domain.Infrastructure.Configuration
{
    public class InMemoryConfigurationRepository : IConfigurationRepository
    {
        private readonly Dictionary<string, IConfiguration> _map;

        public InMemoryConfigurationRepository()
        {
            _map = new Dictionary<string, IConfiguration>();
        }

        public T GetConfig<T>() where T : IConfiguration
        {
            return (T) _map[nameof(T)];
        }

        public void AddConfig<T>(T configuration) where T : IConfiguration
        {
            _map.Add(nameof(T), configuration);
        }
    }
}
