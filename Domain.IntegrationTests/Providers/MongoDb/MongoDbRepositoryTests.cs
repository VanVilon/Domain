using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Infrastructure.Helpers;
using Domain.IntegrationTests.TestHelpers;
using Domain.Persistence.Providers.MongoDb;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;

namespace Domain.IntegrationTests.Providers.MongoDb
{
    [TestClass]
    public class MongoDbRepositoryTests : IDisposable
    {
        public MongoDbRepositoryTests()
        {
            try
            {
                this.Connect();
            }
            catch (Exception e)
            {
                throw new TestCanceledException("Could not connect to the test mongoDb server. See inner exception", e);
            }
        }

        private bool Connect()
        {
            var connectionString = $"mongodb://test:123";
            var localMongoClient = new MongoClient(connectionString);
            var testDatabase = localMongoClient.GetDatabase("test");
            //TODO connect to test database and create it if not exist.
        }

        [TestMethod]
        public async Task Find_aggregate_by_id()
        {
            var repository = new TestMongoRepository();

            var result = await repository.FindByIdAsync(new Guid());

            Assert.IsNull(result); 
        }

        [TestMethod]
        public void Find_aggregate_by_property()
        {
            var repository = new TestMongoRepository();

            var result = repository.FindOne(aggregate => aggregate.Number == 1);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Find_multiple_aggregates_by_property()
        {
            var repository = new TestMongoRepository();

            var result = repository.Find(aggregate => aggregate.Number == 1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Add_aggregate_stored_in_collection_cache()
        {
            var repository = new TestMongoRepository();

            var testAggregate = new TestAggregate();

            repository.Add(testAggregate);

            Assert.IsTrue(repository.CurrentState.Contains(new InsertedAggregate(testAggregate)));
        }

        [TestMethod]
        public async Task Delete_aggregate_from_repository_stored_in_collection_cache()
        {
            var repository = new TestMongoRepository();

            var testAggregate = new TestAggregate();
            
            repository.Remove(testAggregate);

            Assert.IsTrue(repository.CurrentState.Contains(new DeletedAggregate(testAggregate.Id)));
        }

        public void Dispose()
        {

        }
    }

    public class TestMongoRepository : MongoDbRepository<TestAggregate>
    {
        public TestMongoRepository() 
            : base(new MongoDataContext(new MongoClientSettings(), "test"))
        {
            //TODO mock connection
        }
    }
}
