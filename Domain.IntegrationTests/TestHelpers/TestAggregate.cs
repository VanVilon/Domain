using System;
using Domain.Infrastructure;

namespace Domain.IntegrationTests.TestHelpers
{
    public class TestAggregate : AggregateRoot
    {
        public TestAggregate(int number)
        {
            Number = number;
        }

        public int Number { get; protected set; }
    }

    public static class TestAggregateDummyData
    {
        private static TestAggregate dummyData;

        public static TestAggregate Get()
        {
            if(dummyData == null)
                dummyData = new TestAggregate(1);

            return dummyData;
        }
    }
}