using System;
using System.Linq;
using Domain.Infrastructure.Events;
using Domain.IntegrationTests.TestHelpers;
using Domain.Persistence.Providers.MongoDb;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Shouldly;

namespace Domain.IntegrationTests.Providers.MongoDb
{
    [TestClass]
    public class MongoDbRepositoryTests : MongoDbRepositorySpecs
    {
        private TestMongoRepository _repository;
        private string _testDb;

        [TestInitialize]
        public void Initialize()
        {
            _testDb = $"TEST_{Guid.NewGuid()}";
            CreateConnection(_testDb);
            _repository = new TestMongoRepository(new MongoDataContext(ConnectionString, _testDb));
        }

        [TestMethod]
        public void FindById_returns_an_aggregate_by_id()
        {
            //Given
            var testAggregate = new TestAggregate(1);
            Collection.InsertMany(new[] { testAggregate, /*noise*/ new TestAggregate(666) });

            //When
            Collection.Find(aggregate => aggregate.Id == testAggregate.Id).Count().ShouldBe(1);
            var result = _repository.FindById(testAggregate.Id);

            //Then
            result.Id.ShouldBe(testAggregate.Id);
            result.Number.ShouldBe(testAggregate.Number);
        }

        [TestMethod]
        public void FindOne_returns_an_aggregate_with_expected_data()
        {
            //Given
            var testAggregate = new TestAggregate(1);
            Collection.InsertMany(new[] {testAggregate, /*noise*/ new TestAggregate(666)});

            //When
            Collection.Find(aggregate => aggregate.Number == 1).Count().ShouldBe(1);
            var result = _repository.FindOne(aggregate => aggregate.Number == 1);

            //Then
            result.Id.ShouldBe(testAggregate.Id);
            result.Number.ShouldBe(testAggregate.Number);
        }

        [TestMethod]
        public void Find_returns_aggregates_with_expected_data()
        {
            //Given
            var ta1 = new TestAggregate(1);
            var ta2 = new TestAggregate(2);
            var guids = new[] { ta1.Id, ta2.Id };

            Collection.InsertMany(new[] { ta1, ta2, /*noise*/ new TestAggregate(666) });

            //When
            Collection.Find(aggregate => guids.Contains(aggregate.Id)).Count().ShouldBe(2);
            var result = _repository.Find(aggregate => guids.Contains(aggregate.Id)).ToList();

            //Then
            result.Count.ShouldBe(2);
            guids.All(guid => result.Any(y => y.Id == guid)).ShouldBe(true);
            result.First(x => x.Id == ta1.Id).Number.ShouldBe(1);
            result.First(x => x.Id == ta2.Id).Number.ShouldBe(2);
        }

        [TestMethod]
        public void Add_inserts_the_aggregate_to_mongo_collection()
        {
            //Given
            var testAggregate = new TestAggregate(1);

            Collection.InsertOne(/*noise*/ new TestAggregate(666));

            //When
            Collection.Find(aggregate => aggregate.Id == testAggregate.Id).Count().ShouldBe(0);
            _repository.Add(testAggregate);
            var result = Collection.Find(aggregate => aggregate.Id == testAggregate.Id).Single();

            //Then
            result.Id.ShouldBe(testAggregate.Id);
            result.Number.ShouldBe(testAggregate.Number);
        }

        [TestMethod]
        public void Remove_deletes_the_aggregate_from_mongo_collection()
        {
            //Given
            var testAggregate = new TestAggregate(1);

            Collection.InsertMany(new[] { testAggregate, /*noise*/ new TestAggregate(666) });

            //When
            Collection.Find(aggregate => aggregate.Id == testAggregate.Id).Count().ShouldBe(1);
            _repository.Remove(testAggregate);
            var result = Collection.Find(aggregate => aggregate.Id == testAggregate.Id).FirstOrDefault();

            //Then
            result.ShouldBeNull();
        }

        [TestMethod]
        public void Update_updates_the_aggregate_in_mongo_collection()
        {
            //Given
            var testAggregate = new TestAggregate(1);

            Collection.InsertMany(new[] { testAggregate, /*noise*/ new TestAggregate(666) });

            //When
            Collection.Find(aggregate => aggregate.Id == testAggregate.Id).Count().ShouldBe(1);

            testAggregate.Apply(new NumberChanged(999));

            _repository.Update(testAggregate);
            var result = Collection.Find(aggregate => aggregate.Id == testAggregate.Id).Single();

            //Then
            result.Id.ShouldBe(testAggregate.Id);
            result.Number.ShouldBe(testAggregate.Number);
        }

        [TestCleanup]
        public void CleanUp()
        {
            Collection.Database.DropCollection(_testDb);
            _testDb = null;
        }
    }

    public class NumberChanged : IDomainEvent
    {
        public int Number { get; }

        public NumberChanged(int number)
        {
            Number = number;
        }
    }

    public abstract class MongoDbRepositorySpecs
    {
        protected static string ConnectionString;
        protected static MongoCollectionBase<TestAggregate> Collection;

        internal static void CreateConnection(string dbName)
        {
            ConnectionString = @"mongodb://localhost:27017";

            var mongoClient = new MongoClient(ConnectionString);
            var db = mongoClient.GetDatabase(dbName);

            Collection = (MongoCollectionBase<TestAggregate>) db.GetCollection<TestAggregate>(nameof(TestAggregate));
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