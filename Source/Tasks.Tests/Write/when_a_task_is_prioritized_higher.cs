using System;
using Machine.Specifications;
using Tasks.Events;
using Tasks.Write.Commands;
using System.Linq;

namespace Tasks.Tests.Write
{
    public class when_a_task_is_prioritized_higher : WriteContext
    {
        static PrioritizeTask _command;

        Establish context = () =>
            {
                var userRegistered = new UserRegistered(Guid.NewGuid(), "prior-higher-test@test.dk", "1234");

                var task1Created = new TaskCreated("task 1", Guid.NewGuid(), userRegistered.UserId, DateTime.Now);
                var task2Created = new TaskCreated("task 2", Guid.NewGuid(), userRegistered.UserId, DateTime.Now);
             
                AddToHistory(userRegistered.UserId, userRegistered);
                AddToHistory(task1Created.TaskId, task1Created);
                AddToHistory(task2Created.TaskId, task2Created);

                _command = new PrioritizeTask(userRegistered.UserId, task2Created.TaskId, task1Created.TaskId, TaskRelativePriority.PrioritizedHigher, DateTime.Now);
            };

        Because of = () => _executor.Execute(_command);

        It should_publish_one_event = () => _eventsPublished.Count.ShouldEqual(1);

        It should_publish_a_task_prioritized_event = () => _eventsPublished.OfType<TaskPrioritized>().Count().ShouldEqual(1);
    }
}