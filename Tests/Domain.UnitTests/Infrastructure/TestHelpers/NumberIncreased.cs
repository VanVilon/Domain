using Domain.Infrastructure.Events;

namespace Domain.UnitTests.Infrastructure.TestHelpers
{
    internal class NumberIncreased : IDomainEvent
    {
        public int Number { get; }

        public NumberIncreased(int number)
        {
            this.Number = number;
        }
    }
}