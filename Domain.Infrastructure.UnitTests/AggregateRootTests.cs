using System;
using System.Collections.Generic;
using System.Text;
using Domain.Infrastructure.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Infrastructure.UnitTests
{
    [TestClass]
    public class AggregateRootTests
    {
        [TestMethod]
        public void Aggregate_command_raising_event___impl_test()
        {
            var aggregate = new TestAggregate();

            Assert.AreEqual(0, aggregate.Number);

            aggregate.IncreaseNumber(2);
            aggregate.IncreaseNumber(1);

            Assert.AreEqual(3, aggregate.Number);
        }

        [TestMethod]
        public void Can_apply_supported_event_to_aggregate()
        {
            var aggregate = new TestAggregate();

            Assert.AreEqual(0, aggregate.Number);

            aggregate.Apply(new NumberIncreased(2));
            
            Assert.AreEqual(2, aggregate.Number);
        }
    }

    internal class TestAggregate : AggregateRoot
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

    internal class NumberIncreased : IDomainEvent
    {
        public int Number { get; }

        public NumberIncreased(int number)
        {
            this.Number = number;
        }
    }
}
