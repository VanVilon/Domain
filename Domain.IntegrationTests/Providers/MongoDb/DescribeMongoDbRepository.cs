using System;
using System.Linq;
using Domain.Infrastructure;
using Domain.IntegrationTests.TestHelpers;
using Domain.Persistence.Providers.MongoDb;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mongo2Go;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Shouldly;

namespace Domain.IntegrationTests.Providers.MongoDb
{
    public class DescribeMongoDbRepository : MongoIntegrationTestsBase
    {
        private static TestMongoRepository _repository;

        [TestInitialize]
        public void BeforeAll()
        {
            CreateConnection();
            _repository = new TestMongoRepository(new MongoDataContext(ConnectionString, "test"));
        }

        [TestClass]
        public class FindById : DescribeMongoDbRepository
        {
            private static TestAggregate _result;
            private TestAggregate _dummyData;

            [TestInitialize]
            public void Before()
            {
                _dummyData = TestAggregateDummyData.Get();
            }

            [TestMethod]
            public void Given_id_when_aggregate_exists_it_collects_aggregate_from_db_with_expected_data()
            {
                var existingAggregate = Collection.Find(aggregate => aggregate.Id == _dummyData.Id).Single();
                existingAggregate.ShouldNotBeNull();

                _result = _repository.FindById(_dummyData.Id);
                _result.ShouldNotBeNull();
                _result.Id.ShouldBe(_dummyData.Id);
                _result.Number.ShouldBe(_dummyData.Number);
            }
        }

        [ClassCleanup]
        public static void Cleanup()
        {
        }
    }

    public abstract class MongoIntegrationTestsBase
    {
        protected static string ConnectionString;
        protected static MongoCollectionBase<TestAggregate> Collection;

        internal static void CreateConnection()
        {
            ConnectionString = @"mongodb://localhost:27017";

            var mongoClient = new MongoClient(ConnectionString);
            var db = mongoClient.GetDatabase("test");

            BsonClassMap.RegisterClassMap<TestAggregate>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(c => c.Number);
            });

            //MongoDefaults.GuidRepresentation = GuidRepresentation.Standard;

            Collection = (MongoCollectionBase<TestAggregate>) db.GetCollection<TestAggregate>(nameof(TestAggregate));

            //Dummy data init
            Collection.InsertOne(TestAggregateDummyData.Get());
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