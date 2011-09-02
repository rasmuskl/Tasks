using System;
using EventStore;
using Tasks.Write.Commands;

namespace Tasks.Write.CommandHandlers
{
    public class MoveTaskToContextHandler : ICommandHandler<MoveTaskToContext>
    {
        public void Handle(MoveTaskToContext command)
        {
            using (IEventStream stream = Storage.Store.OpenStream(command.TaskId, 0, int.MaxValue))
            {
                var task = new Task();

                foreach (var committedEvent in stream.CommittedEvents)
                {
                    task.ApplyCommitted(committedEvent.Body);
                }

                task.MoveToContext(command.UserId, command.TargetContextId);

                foreach (var uncommittedEvent in task.UncommittedEvents)
                {
                    stream.Add(new EventMessage { Body = uncommittedEvent });
                }

                stream.CommitChanges(Guid.NewGuid());
            }
        }
    }
}