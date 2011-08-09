using System;
using EventStore;
using Tasks.Model.Commands;

namespace Tasks.Model.CommandHandlers
{
    public class CreateTaskHandler : IHandle<CreateTaskCommand>
    {
        public void Handle(CreateTaskCommand command)
        {
            using(IEventStream stream = Storage.Store.CreateStream(command.TaskId))
            {
                var task = new Task();

                task.CreateTask(command.Task, command.TaskId);

                foreach (var uncommittedEvent in task.UncommittedEvents)
                {
                    stream.Add(new EventMessage { Body = uncommittedEvent });
                }

                stream.CommitChanges(Guid.NewGuid());
            }
        }
    }
}