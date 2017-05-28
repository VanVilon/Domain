using Domain.Infrastructure;

namespace Domain.UnitTests.Infrastructure.TestHelpers
{
    public class TestAggregate : AggregateRoot
    {
        public TestAggregate()
        {
            this.RegisterHandler<NumberIncreased>(HandleNumberIncreasedEvent);
        }

        public int Number { get; set; }

        private void HandleNumberIncreasedEvent(NumberIncreased @event)
        {
            Number += @event.Number;
        }

        public void IncreaseNumber(int number)
        {
            Apply(new NumberIncreased(number));
        }
    }
}