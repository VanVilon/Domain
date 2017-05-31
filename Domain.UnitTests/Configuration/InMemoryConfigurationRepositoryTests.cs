using System;
using System.Collections.Generic;
using System.Text;
using Domain.Infrastructure.Configuration;
using Domain.Infrastructure.Persistence.Providers.DefaultProvider;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.UnitTests.Configuration
{
    [TestClass]
    public class InMemoryConfigurationRepositoryTests
    {
        public void Add_new_configuration__configuration_should_persisted_and_be_available_for_usage()
        {
            var repository = new InMemoryConfigurationRepository();
            var testConfiguration = new TestConfiguration{SomeSpec = "test"};
            repository.AddConfig(testConfiguration);

            var configuration = repository.GetConfig<TestConfiguration>();

            Assert.AreEqual(testConfiguration, configuration);
        }
    }
}
