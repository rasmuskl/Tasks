using System.Linq;
using System.Collections.Generic;
using System;
using Machine.Specifications;
using Tasks.Events;
using Tasks.Read;
using Tasks.Read.Queries;

namespace Tasks.Tests.Read
{
    public class when_a_task_is_completed : ReadContext
    {
        static TaskCompleted _taskCompleted;

        Establish context = () =>
            {
                var userRegistered = ProcessedEvent(new UserRegistered(Guid.NewGuid(), "note-test@test.dk", "1234"));
                var taskCreated = ProcessedEvent(new TaskCreated("task 1", Guid.NewGuid(), userRegistered.UserId, DateTime.UtcNow));

                _taskCompleted = new TaskCompleted(DateTime.UtcNow, userRegistered.UserId, taskCreated.TaskId);
            };

        Because of = () => ProcessedEvent(_taskCompleted);

        It should_not_find_note_in_general_context = () =>
            ReadStorage.Query(new QueryTasksByContextId(_taskCompleted.UserId, Guid.Empty))
                .ShouldNotContain(x => x.TaskId == _taskCompleted.TaskId);

        It should_be_in_recently_completed = () =>
            ReadStorage.Query(new QueryRecentlyCompletedTasks(_taskCompleted.UserId))
                .ShouldContain(x => x.TaskId == _taskCompleted.TaskId);

        It should_be_in_recently_completed_general = () =>
            ReadStorage.Query(new QueryRecentlyCompletedTasksByContextId(_taskCompleted.UserId, Guid.Empty))
                .ShouldContain(x => x.TaskId == _taskCompleted.TaskId);

        It should_not_be_in_recently_completed_other = () =>
            ReadStorage.Query(new QueryRecentlyCompletedTasksByContextId(_taskCompleted.UserId, Guid.NewGuid()))
                .ShouldNotContain(x => x.TaskId == _taskCompleted.TaskId);
    }
}