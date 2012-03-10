using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using Tasks.Events;
using Tasks.Read;
using Tasks.Read.Queries;
using Machine.Specifications;

namespace Tasks.Tests.Read
{
    public class TaskNestedFixture : NUReadContext
    {
        private TaskNested _taskNested;

        protected override void Given()
        {
            var userRegistered = ProcessedEvent(new UserRegistered(Guid.NewGuid(), Guid.NewGuid() + "@test.dk", "1234"));
            var task1Created = ProcessedEvent(new TaskCreated("Task 1", Guid.NewGuid(), userRegistered.UserId, Guid.Empty, DateTime.UtcNow));
            var task2Created = ProcessedEvent(new TaskCreated("Task 2", Guid.NewGuid(), userRegistered.UserId, Guid.Empty, DateTime.UtcNow));

            _taskNested = new TaskNested(userRegistered.UserId, task2Created.TaskId, task1Created.TaskId, DateTime.UtcNow);
        }

        protected override void When()
        {
            ProcessedEvent(_taskNested);
        }

        [Test]
        public void NestedTaskShouldStillBeFindable()
        {
            ReadStorage.Query(new QueryTaskById(_taskNested.UserId, _taskNested.TaskId)).ShouldNotBeNull();
        }

        [Test]
        public void ParentTaskShouldStillBeFindable()
        {
            ReadStorage.Query(new QueryTaskById(_taskNested.UserId, _taskNested.ParentTaskId)).ShouldNotBeNull();
        }

        [Test]
        public void ParentTaskShouldContainNewChild()
        {
            var parentTask = ReadStorage.Query(new QueryTaskById(_taskNested.UserId, _taskNested.ParentTaskId));
            parentTask.NestedTasks.ShouldContain(x => x.TaskId == _taskNested.TaskId);
        }

        [Test]
        public void ChildTaskShouldNoLongerAppearInContextRoot()
        {
            var tasks = ReadStorage.Query(new QueryTasksByContextId(_taskNested.UserId, Guid.Empty));
            tasks.ShouldNotContain(x => x.TaskId == _taskNested.TaskId);
        }
    }
}