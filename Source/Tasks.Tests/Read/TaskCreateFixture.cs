using System;
using Machine.Specifications;
using NUnit.Framework;
using Tasks.Events;
using Tasks.Read;
using Tasks.Read.Queries;

namespace Tasks.Tests.Read
{
    public class TaskCreateFixture : NUReadContext
    {
        private static TaskCreated _taskCreated;

        protected override void Given()
        {
            var userRegistered = ProcessedEvent(new UserRegistered(Guid.NewGuid(), "task-test@test.dk", "1234"));
            _taskCreated = new TaskCreated("Task 1", Guid.NewGuid(), userRegistered.UserId, Guid.Empty, DateTime.Now);
        }

        protected override void When()
        {
            ProcessedEvent(_taskCreated);
        }

        [Test]
        public void ShouldFindTaskInGeneralContext()
        {
            ReadStorage.Query(new QueryTasksByContextId(_taskCreated.UserId, Guid.Empty))
            .ShouldContain(x => x.TaskId == _taskCreated.TaskId);
        }

        [Test]
        public void ShouldNotFindTaskInAnotherContext()
        {
            ReadStorage.Query(new QueryTasksByContextId(_taskCreated.UserId, Guid.NewGuid()))
            .ShouldNotContain(x => x.TaskId == _taskCreated.TaskId);
        }

        [Test]
        public void ShouldExistAsATaskForTheUser()
        {
            ReadStorage.Query(new QueryUserHasTask(_taskCreated.UserId, _taskCreated.TaskId)).ShouldBeTrue();
        }

        [Test]
        public void ShouldBeFindableById()
        {
            ReadStorage.Query(new QueryTaskById(_taskCreated.UserId, _taskCreated.TaskId)).ShouldNotBeNull();
        }
    }
}