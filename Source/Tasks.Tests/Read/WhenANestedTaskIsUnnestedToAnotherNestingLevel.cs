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
    public class WhenANestedTaskIsUnnestedToAnotherNestingLevel : NUReadContext
    {
        private TaskNested _taskNested;
        private Guid _previousParentTaskId;

        // FROM:
        //  - task 1
        //      - task 2
        //          - task 3
        //      - task 4
        //
        // TO:
        //  - task 1
        //      - task 2
        //      - task 3
        //      - task 4

        protected override void Given()
        {
            var userRegistered = ProcessedEvent(new UserRegistered(Guid.NewGuid(), Guid.NewGuid() + "@test.dk", "1234"));
            var task1Created = ProcessedEvent(new TaskCreated("Parent", Guid.NewGuid(), userRegistered.UserId, Guid.Empty, DateTime.UtcNow));
            var task2Created = ProcessedEvent(new TaskCreated("Child 1", Guid.NewGuid(), userRegistered.UserId, Guid.Empty, DateTime.UtcNow));
            var task3Created = ProcessedEvent(new TaskCreated("Child 2", Guid.NewGuid(), userRegistered.UserId, Guid.Empty, DateTime.UtcNow));
            var task4Created = ProcessedEvent(new TaskCreated("Child 3", Guid.NewGuid(), userRegistered.UserId, Guid.Empty, DateTime.UtcNow));
            ProcessedEvent(new TaskNested(userRegistered.UserId, task2Created.TaskId, task1Created.TaskId, DateTime.UtcNow));
            ProcessedEvent(new TaskNested(userRegistered.UserId, task3Created.TaskId, task2Created.TaskId, DateTime.UtcNow));
            ProcessedEvent(new TaskNested(userRegistered.UserId, task4Created.TaskId, task1Created.TaskId, DateTime.UtcNow));

            _previousParentTaskId = task2Created.TaskId;

            _taskNested = new TaskNested(userRegistered.UserId, task3Created.TaskId, task1Created.TaskId, DateTime.UtcNow);
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
        public void ChildTaskShouldAppearInAfterOldParentInNewParent()
        {
            var task = ReadStorage.Query(new QueryTaskById(_taskNested.UserId, _taskNested.TaskId));
            var previousParent = ReadStorage.Query(new QueryTaskById(_taskNested.UserId, _previousParentTaskId));

            var tasks = ReadStorage.Query(new QueryTaskById(_taskNested.UserId, _taskNested.ParentTaskId)).NestedTasks;

            tasks.IndexOf(task).ShouldEqual(1 + tasks.IndexOf(previousParent));
        }

        [Test]
        public void ChildTaskShouldNoLongerAppearInPreviouslyNestedTask()
        {
            var previousParentTask = ReadStorage.Query(new QueryTaskById(_taskNested.UserId, _previousParentTaskId));
            previousParentTask.NestedTasks.ShouldNotContain(x => x.TaskId == _taskNested.TaskId);
        }
    }
}