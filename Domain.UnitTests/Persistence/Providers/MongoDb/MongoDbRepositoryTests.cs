using System;
using System.Threading.Tasks;
using Domain.Infrastructure;
using Domain.Infrastructure.Helpers;
using Domain.Persistence.Providers.MongoDb;
using Domain.UnitTests.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Moq;
using NSubstitute;

namespace Domain.UnitTests.Persistence.Providers.MongoDb
{
    [TestClass]
    public class MongoDbRepositoryTests
    {

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
    }

    public class TestMongoRepository : MongoDbRepository<TestAggregate>
    {
        public TestMongoRepository() 
            : base(new MongoDataContext(new MongoClientSettings(), "testDb"))
        {
            //TODO mock connection
        }

        public UniqueQueue<IMongoRepositoryChange> CurrentState => base.Changes;
    }
}
