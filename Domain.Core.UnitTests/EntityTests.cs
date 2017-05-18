using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Core.UnitTests
{
    [TestClass]
    public class EntityTests
    {
        [TestMethod]
        public void Entity_provides_unique_identifier()
        {
            var identity = Guid.NewGuid();
            var testEntity = new Product(identity);

            Assert.IsTrue(testEntity.GetIdentityComponents().Contains(identity));
        }
    }

    internal class Product : EntityWithCompositeId
    {
        //early id
        private readonly Guid _identity;

        public Product(Guid identity)
        {
            _identity = identity;
        }

        public override IEnumerable<object> GetIdentityComponents()
        {
            return new List<object> { _identity };
        }
    }
}
