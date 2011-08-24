using System;
using EventStore;
using Tasks.Write.Commands;

namespace Tasks.Write.CommandHandlers
{
    public class CreateTaskHandler : IHandle<CreateTask>
    {
        public void Handle(CreateTask command)
        {
            using(IEventStream stream = Storage.Store.CreateStream(command.TaskId))
            {
                var task = new Task();

                task.CreateTask(command.Title, command.TaskId);

                foreach (var uncommittedEvent in task.UncommittedEvents)
                {
                    stream.Add(new EventMessage { Body = uncommittedEvent });
                }

                stream.CommitChanges(Guid.NewGuid());
            }
        }
    }
}