using System;
using Machine.Specifications;
using NUnit.Framework;
using Tasks.Events;
using Tasks.Read;
using Tasks.Read.Queries;
using System.Linq;

namespace Tasks.Tests.Read
{
    public class WhenAContextIsCreated : NUReadContext
    {
        private ContextCreated _contextCreated;

        protected override void Given()
        {
            var userRegistered = ProcessedEvent(new UserRegistered(Guid.NewGuid(), "context-test" + Guid.NewGuid() + "@test.dk", "1234"));
            _contextCreated = new ContextCreated(Guid.NewGuid(), "Context 1", userRegistered.UserId, DateTime.Now);
        }

        protected override void When()
        {
            ProcessedEvent(_contextCreated);
        }

        [Test]
        public void ShouldFindContextByUserId()
        {
            ReadStorage.Query(new QueryContextsByUserId(_contextCreated.UserId))
                .Count(x => x.ContextId == _contextCreated.ContextId)
                .ShouldEqual(1);
        }

        [Test]
        public void ShouldFindAContextIdByNameForUser()
        {
            ReadStorage.Query(new QueryContextIdByName(_contextCreated.UserId, "Context 1"))
                .ShouldEqual(_contextCreated.ContextId);
        }

        [Test]
        public void ShouldFindAContextIdByDifferentlyCasedNameForUser()
        {
            ReadStorage.Query(new QueryContextIdByName(_contextCreated.UserId, "CONTEXT 1"))
                .ShouldEqual(_contextCreated.ContextId);
        }

        [Test]
        public void ShouldFindAContextByNameForUser()
        {
            ReadStorage.Query(new QueryUserHasContextNamed(_contextCreated.UserId, "Context 1"))
                .ShouldBeTrue();
        }

        [Test]
        public void ShouldFindAContextByDifferentlyCasedNameForUser()
        {
            ReadStorage.Query(new QueryUserHasContextNamed(_contextCreated.UserId, "CONTEXT 1"))
                .ShouldBeTrue();
        }

        [Test]
        public void ShouldFindContextById()
        {
            ReadStorage.Query(new QueryContextById(_contextCreated.UserId, _contextCreated.ContextId))
                .ShouldNotBeNull();
        }

        [Test]
        public void ShouldFindContextWhenGettingContextsExceptGeneralContext()
        {
            ReadStorage.Query(new QueryContextsExceptContext(_contextCreated.UserId, Guid.Empty))
                .First()
                .ContextId
                .ShouldEqual(_contextCreated.ContextId);
        }

        [Test]
        public void ShouldNotFindContextWhenGettingContextsExceptNewContext()
        {
            ReadStorage.Query(new QueryContextsExceptContext(_contextCreated.UserId, _contextCreated.ContextId))
                .ShouldNotContain(x => x.ContextId == _contextCreated.ContextId);
        }
    }
}