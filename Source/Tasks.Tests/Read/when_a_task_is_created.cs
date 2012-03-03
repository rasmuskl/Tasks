using System;
using Machine.Specifications;
using Tasks.Events;
using Tasks.Read;
using Tasks.Read.Queries;

namespace Tasks.Tests.Read
{
    public class when_a_task_is_created : ReadContext
    {
        static TaskCreated _taskCreated;

        Establish context = () =>
            {
                var userRegistered = ProcessedEvent(new UserRegistered(Guid.NewGuid(), "task-test@test.dk", "1234"));

                _taskCreated = new TaskCreated("Task 1", Guid.NewGuid(), userRegistered.UserId, Guid.Empty, DateTime.Now);
            };

        Because of = () => ProcessedEvent(_taskCreated);

        It should_find_task_in_general_context = () =>
            ReadStorage.Query(new QueryTasksByContextId(_taskCreated.UserId, Guid.Empty))
            .ShouldContain(x => x.TaskId == _taskCreated.TaskId);

        It should_not_find_task_in_another_context = () =>
            ReadStorage.Query(new QueryTasksByContextId(_taskCreated.UserId, Guid.NewGuid()))
            .ShouldNotContain(x => x.TaskId == _taskCreated.TaskId);

        It should_exist_as_a_task_for_the_user = () =>
            ReadStorage.Query(new QueryUserHasTask(_taskCreated.UserId, _taskCreated.TaskId)).ShouldBeTrue();
    }
}