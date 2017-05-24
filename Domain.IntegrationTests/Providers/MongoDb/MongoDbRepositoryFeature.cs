using Domain.IntegrationTests.TestHelpers;
using Domain.Persistence.Providers.MongoDb;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mongo2Go;
using MongoDB.Driver;
using Shouldly;
using TechTalk.SpecFlow;

namespace Domain.IntegrationTests.Providers.MongoDb
{
    [Binding]
    public class MongoDbRepositoryFeature : MongoIntegrationTests
    {
        private TestMongoRepository _repository;
        private TestAggregate _result;
        private readonly TestAggregate _dummyData = TestAggregate.DummyData();

        [Given]
        public void Given_a_mongo_repository()
        {
            _repository = new TestMongoRepository(new MongoDataContext(Runner.ConnectionString, "test"));
        }

        [When]
        public void When_aggregate_exists_in_the_mongo_collection_and_calling_FindById()
        {
            _result = _repository.FindById(_dummyData.Id);
            Assert.IsNotNull(Collection.Find(aggregate => aggregate.Id == _dummyData.Id));
        }

        [Then]
        public void Then_returns_the_aggregate_with_expected_data()
        {
            _result.ShouldNotBeNull();
            _result.Id.ShouldBe(_dummyData.Id);
            _result.Number.ShouldBe(_dummyData.Number);
        }

        [ClassCleanup]
        public void Cleanup()
        {
            Runner.Dispose();
        }
    }

    public class MongoIntegrationTests
    {
        protected static MongoDbRunner Runner;
        protected static IMongoCollection<TestAggregate> Collection;

        internal static void CreateConnection()
        {
            Runner = MongoDbRunner.Start();

            var mongoClient = new MongoClient(Runner.ConnectionString);
            var db = mongoClient.GetDatabase("test");

            Collection = db.GetCollection<TestAggregate>("TestAggregate");

            //Dummy data init
            Collection.InsertOne(TestAggregate.DummyData());
        }
    }

    public class TestMongoRepository : MongoDbRepository<TestAggregate>
    {
        public TestMongoRepository(MongoDataContext mongoDataContext) 
            : base(mongoDataContext)
        {
            //TODO mock connection
        }
    }
}
