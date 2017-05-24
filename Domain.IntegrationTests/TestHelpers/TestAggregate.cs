using Domain.Infrastructure;

namespace Domain.IntegrationTests.TestHelpers
{
    public class TestAggregate : AggregateRoot
    {
        public TestAggregate(int number)
        {
            Number = number;
        }

        public int Number { get; }

        public static TestAggregate DummyData()
        {
            return new TestAggregate(1);
        }
    }
}