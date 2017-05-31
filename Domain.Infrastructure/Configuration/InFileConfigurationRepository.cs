using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.Infrastructure.Persistence.Providers.DefaultProvider;
using Newtonsoft.Json;

namespace Domain.Infrastructure.Configuration
{
    public class InFileConfigurationRepository : IConfigurationRepository
    {
        private readonly string _configFilePath;
        private readonly HashSet<RawConfig> _set;

        public InFileConfigurationRepository(string configFilePath)
        {
            if(string.IsNullOrEmpty(configFilePath))
                throw new InvalidOperationException("Path cannot be null");

            _configFilePath = configFilePath;

            if (!File.Exists(configFilePath))
                throw new InvalidOperationException($"Could not load config file ({configFilePath})");
            
            var content = File.ReadAllText(configFilePath);

            try
            {
                _set = JsonConvert.DeserializeObject<HashSet<RawConfig>>(content) ?? new HashSet<RawConfig>();
            }
            catch (JsonException e)
            {
                throw new InvalidOperationException("File was not in a correct format. See inner exception", e);
            }
        }

        public T GetConfig<T>() where T : IConfiguration
        {
            Type type = typeof(T);
            string typeName = type.ToString();
            var config = _set.FirstOrDefault(o => o.Type == typeName); //From cache

            if (config != null)
                return JsonConvert.DeserializeObject<T>(config.Value);

            return default(T);
        }

        public void AddConfig<T>(T configuration) where T : IConfiguration
        {
            Type type = typeof(T);
            string typeName = type.ToString();

            _set.Add(new RawConfig
            {
                Name = nameof(T),
                Type = typeName,
                Value = JsonConvert.SerializeObject(configuration)
            });

            File.WriteAllText(_configFilePath, JsonConvert.SerializeObject(_set));
        }
    }
}