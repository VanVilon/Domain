using System;
using System.Linq;
using Domain.Infrastructure;
using Domain.IntegrationTests.TestHelpers;
using Domain.Persistence.Providers.MongoDb;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Shouldly;

namespace Domain.IntegrationTests.Providers.MongoDb
{
    [TestClass]
    public class DescribeMongoDbRepository : MongoDbRepositorySpecs
    {
        [TestMethod]
        public void Given_db_connection_when_calling_FindById_and_aggregate_exists_finds_an_aggregate_by_id()
        {
            var testAggregate = TestAggregateDummyData.Get();
            //Given
            CreateConnection();
            var repository = new TestMongoRepository(new MongoDataContext(ConnectionString, "test"));

            //When
            Collection.Find(aggregate => aggregate.Id == testAggregate.Id).Single().ShouldNotBeNull();
            var result = repository.FindById(testAggregate.Id);

            //Then
            result.Id.ShouldBe(testAggregate.Id);
            result.Number.ShouldBe(testAggregate.Number);
        }
    }

    public abstract class MongoDbRepositorySpecs
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

            Collection = (MongoCollectionBase<TestAggregate>) db.GetCollection<TestAggregate>(nameof(TestAggregate));

            //Dummy data init
            Collection.InsertOne(TestAggregateDummyData.Get());
        }

        internal static void DestroyTestDb(){
            Collection.Database.DropCollection(nameof(TestAggregate));
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