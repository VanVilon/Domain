using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Domain.Infrastructure.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.UnitTests.Configuration
{
    [TestClass]
    public class InFileConfigurationRepositoryTests
    {
        [TestMethod]
        public void Add_new_configuration__configuration_should_persisted_and_be_available_for_usage()
        {
            var path = CreateMockFile();

            var repository = new InFileConfigurationRepository(path);
            var testConfiguration = new TestConfiguration { SomeSpec = "test" };
            repository.AddConfig(testConfiguration);

            var configuration = repository.GetConfig<TestConfiguration>();
            
            Assert.AreEqual(testConfiguration.SomeSpec, configuration.SomeSpec);
        }

        private static string CreateMockFile()
        {
            var tempPath = @"test.txt";
            File.WriteAllText(tempPath, string.Empty);

            return tempPath;
        }

        [TestCleanup]
        public void CleanUp()
        {
            if(File.Exists(@"test.txt"))
                File.Delete(@"test.txt");
        }
    }
}
