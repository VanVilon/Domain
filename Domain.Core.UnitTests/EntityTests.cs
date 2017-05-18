using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Model;
using Xunit;

namespace Domain.Core.UnitTests
{
    public class EntityTests
    {
        [Fact]
        public void Entity_provides_unique_identifier()
        {
            var identity = Guid.NewGuid();
            var testEntity = new Product(identity);

            Assert.True(testEntity.GetIdentityComponents().Contains(identity));
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
