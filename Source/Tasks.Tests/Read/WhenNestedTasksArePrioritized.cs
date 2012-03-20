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
    public class WhenNestedTasksArePrioritized : NUReadContext
    {
        private TaskPrioritized _taskPrioritized;
        private Guid _parentTaskId;

        // FROM:
        //  - task 1
        //      - task 2
        //      - task 3
        //
        // TO:
        //  - task 1
        //      - task 3
        //      - task 2

        protected override void Given()
        {
            var userRegistered = ProcessedEvent(new UserRegistered(Guid.NewGuid(), Guid.NewGuid() + "@test.dk", "1234"));
            var task1Created = ProcessedEvent(new TaskCreated("Parent", Guid.NewGuid(), userRegistered.UserId, Guid.Empty, DateTime.UtcNow));
            var task2Created = ProcessedEvent(new TaskCreated("Child 1", Guid.NewGuid(), userRegistered.UserId, Guid.Empty, DateTime.UtcNow));
            var task3Created = ProcessedEvent(new TaskCreated("Child 2", Guid.NewGuid(), userRegistered.UserId, Guid.Empty, DateTime.UtcNow));
            ProcessedEvent(new TaskNested(userRegistered.UserId, task2Created.TaskId, task1Created.TaskId, DateTime.UtcNow));
            ProcessedEvent(new TaskNested(userRegistered.UserId, task3Created.TaskId, task1Created.TaskId, DateTime.UtcNow));

            _parentTaskId = task1Created.TaskId;

            _taskPrioritized = new TaskPrioritized(userRegistered.UserId, task3Created.TaskId, task2Created.TaskId, TaskRelativePriority.PrioritizedHigher, DateTime.UtcNow);
        }

        protected override void When()
        {
            ProcessedEvent(_taskPrioritized);
        }

        [Test]
        public void NestedTaskShouldStillBeFindable()
        {
            ReadStorage.Query(new QueryTaskById(_taskPrioritized.UserId, _taskPrioritized.MovedTaskId)).ShouldNotBeNull();
        }

        [Test]
        public void TaskShouldAppearBeforeOtherTask()
        {
            ReadStorage.Query(new QueryTaskById(_taskPrioritized.UserId, _parentTaskId)).NestedTasks.First().TaskId.ShouldEqual(_taskPrioritized.MovedTaskId);
        }

    }
}