using System.Collections;
using System.Collections.Generic;
using Domain.UnitTests.Infrastructure.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.UnitTests.Infrastructure
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
}
