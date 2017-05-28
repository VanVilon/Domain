using System;
using Domain.Infrastructure;
using Domain.IntegrationTests.Providers.MongoDb;
using MongoDB.Bson.Serialization;

namespace Domain.IntegrationTests.TestHelpers
{
    public class TestAggregate : AggregateRoot
    {
        public TestAggregate(int number)
        {
            Number = number;
            RegisterHandler<NumberChanged>(Apply);
        }

        private void Apply(NumberChanged e)
        {
            Number = e.Number;
        }

        public int Number { get; protected set; }
    }

    public class TestAggregateMap : BsonClassMap<TestAggregate>
    {
        public TestAggregateMap()
        {
            this.AutoMap();
            this.MapMember(c => c.Number);
        }
    }
}