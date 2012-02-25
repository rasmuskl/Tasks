using System;
using Machine.Specifications;
using NUnit.Framework;
using Tasks.Events;
using Tasks.Read;
using Tasks.Read.Queries;
using System.Linq;

namespace Tasks.Tests.Read
{
    public class WhenAContextIsCreated : SQReadContext
    {
        private ContextCreated _contextCreated;

        public void ARegisteredUser()
        {
            var userRegistered = ProcessedEvent(new UserRegistered(Guid.NewGuid(), "context-test"+Guid.NewGuid()+"@test.dk", "1234"));
            _contextCreated = new ContextCreated(Guid.NewGuid(), "Context 1", userRegistered.UserId, DateTime.Now);
        }

        public void ContextCreatedEventIsProcessed()
        {
            ProcessedEvent(_contextCreated);
        }

        [Test]
        public void ShouldFindContextByUserId()
        {
            Scenario(s => s.Given(ARegisteredUser)
                .When(ContextCreatedEventIsProcessed)
                .Then(UserHasOneContext));
        }

        [Test]
        public void ShouldFindAContextIdByNameForUser()
        {
            Scenario(s => s.Given(ARegisteredUser)
                .When(ContextCreatedEventIsProcessed)
                .Then(UserContextIdCanBeFoundByName));
        }

        [Test]
        public void ShouldFindAContextIdByDifferentlyCasedNameForUser()
        {
            Scenario(s => s.Given(ARegisteredUser)
                .When(ContextCreatedEventIsProcessed)
                .Then(UserContextIdCanBeFoundByDifferentCaseName));
        }        
        
        [Test]
        public void ShouldFindAContextByNameForUser()
        {
            Scenario(s => s.Given(ARegisteredUser)
                .When(ContextCreatedEventIsProcessed)
                .Then(UserContextCanBeFoundByName));
        }

        [Test]
        public void ShouldFindAContextByDifferentlyCasedNameForUser()
        {
            Scenario(s => s.Given(ARegisteredUser)
                .When(ContextCreatedEventIsProcessed)
                .Then(UserContextCanBeFoundByDifferentCaseName));
        }

        [Test]
        public void ShouldFindContextById()
        {
            Scenario(s => s.Given(ARegisteredUser)
                              .When(ContextCreatedEventIsProcessed)
                              .Then(ContextCanBeFoundById));

        }

        [Test]
        public void ShouldFindContextWhenGettingContextsExceptGeneralContext()
        {
            Scenario(s => s.Given(ARegisteredUser)
                              .When(ContextCreatedEventIsProcessed)
                              .Then(UserContextsExceptGeneralContainsContext));
        }

        [Test]
        public void ShouldNotFindContextWhenGettingContextsExceptNewContext()
        {
            Scenario(s => s.Given(ARegisteredUser)
                              .When(ContextCreatedEventIsProcessed)
                              .Then(UserContextsExceptNewDoesNotContainContext));
        }

        private void UserContextIdCanBeFoundByDifferentCaseName()
        {
            ReadStorage.Query(new QueryContextIdByName(_contextCreated.UserId, "CONTEXT 1"))
                .ShouldEqual(_contextCreated.ContextId);
        }

        private void UserContextIdCanBeFoundByName()
        {
            ReadStorage.Query(new QueryContextIdByName(_contextCreated.UserId, "Context 1"))
                .ShouldEqual(_contextCreated.ContextId);
        }

        private void UserHasOneContext()
        {
            ReadStorage.Query(new QueryContextsByUserId(_contextCreated.UserId))
                .Count(x => x.ContextId == _contextCreated.ContextId)
                .ShouldEqual(1);
        }

        private void UserContextCanBeFoundByName()
        {
            ReadStorage.Query(new QueryUserHasContextNamed(_contextCreated.UserId, "Context 1"))
                .ShouldBeTrue();
        }

        private void UserContextCanBeFoundByDifferentCaseName()
        {
            ReadStorage.Query(new QueryUserHasContextNamed(_contextCreated.UserId, "CONTEXT 1"))
                .ShouldBeTrue();
        }

        private void ContextCanBeFoundById()
        {
            ReadStorage.Query(new QueryContextById(_contextCreated.UserId, _contextCreated.ContextId))
                .ShouldNotBeNull();
        }

        private void UserContextsExceptGeneralContainsContext()
        {
            ReadStorage.Query(new QueryContextsExceptContext(_contextCreated.UserId, Guid.Empty))
                .First()
                .ContextId
                .ShouldEqual(_contextCreated.ContextId);
        }

        private void UserContextsExceptNewDoesNotContainContext()
        {
            ReadStorage.Query(new QueryContextsExceptContext(_contextCreated.UserId, _contextCreated.ContextId))
                .ShouldNotContain(x => x.ContextId == _contextCreated.ContextId);
        }
    }
}