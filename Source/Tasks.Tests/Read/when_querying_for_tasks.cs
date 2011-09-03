using System;
using Machine.Specifications;
using Tasks.Events;
using Tasks.Read;
using Tasks.Read.Queries;

namespace Tasks.Tests.Read
{
    public class when_querying_for_tasks : ReadContext
    {
        static TaskCreated _taskCreated;

        Establish context = () =>
            {
                var userRegistered = WithEvent(new UserRegistered(Guid.NewGuid(), "task-test@test.dk", "1234"));

                _taskCreated = new TaskCreated("Task 1", Guid.NewGuid(), userRegistered.UserId, DateTime.Now);
            };

        Because of = () => WithEvent(_taskCreated);

        It should_find_task_in_general_context = () =>
            ReadStorage.Query(new QueryTasksByContextId(_taskCreated.UserId, Guid.Empty))
            .ShouldContain(x => x.TaskId == _taskCreated.TaskId);

        It should_not_find_task_in_another_context = () =>
            ReadStorage.Query(new QueryTasksByContextId(_taskCreated.UserId, Guid.NewGuid()))
            .ShouldNotContain(x => x.TaskId == _taskCreated.TaskId);
    }
}