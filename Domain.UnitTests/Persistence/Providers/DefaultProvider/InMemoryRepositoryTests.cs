using System.Collections.Generic;
using System.Linq;
using Domain.Infrastructure;
using Domain.Infrastructure.Persistence.Providers.DefaultProvider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Domain.UnitTests.Persistence.Providers.DefaultProvider
{
    [TestClass]
    public class InMemoryRepositoryTests
    {
        [TestMethod]
        public void Can_find_entity_by_id()
        {
            var entity = Substitute.For<IAggregate>();
            var id = entity.Id;
            var repository = new InMemoryTestRepository();

            repository.CurrentState.Add(entity);

            var foundEntity = repository.FindById(id);

            Assert.AreEqual(entity, foundEntity);
        }

        [TestMethod]
        public void Can_find_single_entity_by_using_predicate()
        {
            var entity = Substitute.For<IAggregate>();
            var repository = new InMemoryTestRepository();

            repository.CurrentState.Add(entity);

            var foundEntity = repository.FindOne(e => e.Id == entity.Id);

            Assert.AreEqual(entity, foundEntity);
        }

        [TestMethod]
        public void Can_find_entities_by_using_predicate()
        {
            var entity = Substitute.For<IAggregate>();
            var repository = new InMemoryTestRepository();

            repository.CurrentState.Add(entity);

            var foundEntity = repository.Find(e => e.Id == entity.Id).ToList();

            Assert.AreEqual(foundEntity[0], entity);
        }

        [TestMethod]
        public void Can_add_entity_only_once()
        {
            var entity = Substitute.For<IAggregate>();
            var repository = new InMemoryTestRepository();

            repository.Add(entity);

            Assert.AreEqual(1, repository.CurrentState.Count);

            repository.Add(entity);

            Assert.AreEqual(1, repository.CurrentState.Count);
        }

        [TestMethod]
        public void Can_remove_entity()
        {
            var entity = Substitute.For<IAggregate>();
            var repository = new InMemoryTestRepository();

            repository.Add(entity);

            Assert.AreEqual(1, repository.CurrentState.Count);

            repository.Remove(entity);

            Assert.AreEqual(0, repository.CurrentState.Count);
        }

        [TestMethod]
        public void Cant_remove_non_added_entity()
        {
            var entity = Substitute.For<AggregateRoot>();
            var repository = new InMemoryTestRepository();
            
            repository.Add(Substitute.For<AggregateRoot>());

            Assert.AreEqual(1, repository.CurrentState.Count);

            repository.Remove(entity);

            Assert.AreEqual(1, repository.CurrentState.Count);
        }
    }

    internal class InMemoryTestRepository : InMemoryRepository<IAggregate>
    {
        public HashSet<IAggregate> CurrentState => Collection;
    }
}
