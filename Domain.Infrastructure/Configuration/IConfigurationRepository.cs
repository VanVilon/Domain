using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Infrastructure.Configuration
{
    public interface IConfigurationRepository
    {
        T GetConfig<T>() where T : IConfiguration;
        void AddConfig<T>(T configuration) where T : IConfiguration;
    }
}
