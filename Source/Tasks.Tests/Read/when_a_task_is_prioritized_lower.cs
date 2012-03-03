using System;
using System.Linq;
using Machine.Specifications;
using Tasks.Events;
using Tasks.Read;
using Tasks.Read.Queries;

namespace Tasks.Tests.Read
{
    public class when_a_task_is_prioritized_lower : ReadContext
    {
        static TaskPrioritized _taskPrioritized;

        Establish context = () =>
            {
                var userRegistered = ProcessedEvent(new UserRegistered(Guid.NewGuid(), "prior-lower-test@test.dk", "1234"));

                var task1Created = ProcessedEvent(new TaskCreated("task 1", Guid.NewGuid(), userRegistered.UserId, Guid.Empty, DateTime.Now));
                var task2Created = ProcessedEvent(new TaskCreated("task 2", Guid.NewGuid(), userRegistered.UserId, Guid.Empty, DateTime.Now));

                _taskPrioritized = new TaskPrioritized(userRegistered.UserId, task1Created.TaskId, task2Created.TaskId, TaskRelativePriority.PrioritizedLower, DateTime.Now);
            };

        Because of = () => ProcessedEvent(_taskPrioritized);

        It should_appear_after_in_contexts = () => 
            ReadStorage.Query(new QueryTasksByContextId(_taskPrioritized.UserId, Guid.Empty)).First().TaskId.ShouldEqual(_taskPrioritized.RelativeTaskId);
    }
}