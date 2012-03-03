using System;
using Machine.Specifications;
using Tasks.Events;
using Tasks.Read;
using Tasks.Read.Queries;
using System.Linq;

namespace Tasks.Tests.Read
{
    public class when_a_task_is_prioritized_higher : ReadContext
    {
        static TaskPrioritized _taskPrioritized;

        Establish context = () =>
            {
                var userRegistered = ProcessedEvent(new UserRegistered(Guid.NewGuid(), "prior-higher-test@test.dk", "1234"));

                var task1Created = ProcessedEvent(new TaskCreated("task 1", Guid.NewGuid(), userRegistered.UserId, Guid.Empty, DateTime.Now));
                var task2Created = ProcessedEvent(new TaskCreated("task 2", Guid.NewGuid(), userRegistered.UserId, Guid.Empty, DateTime.Now));

                _taskPrioritized = new TaskPrioritized(userRegistered.UserId, task2Created.TaskId, task1Created.TaskId, TaskRelativePriority.PrioritizedHigher, DateTime.Now);
            };

        Because of = () => ProcessedEvent(_taskPrioritized);

        It should_appear_before_in_contexts = () => 
            ReadStorage.Query(new QueryTasksByContextId(_taskPrioritized.UserId, Guid.Empty)).First().TaskId.ShouldEqual(_taskPrioritized.MovedTaskId);
    }
}