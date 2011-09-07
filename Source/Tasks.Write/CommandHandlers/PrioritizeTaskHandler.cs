using System;
using EventStore;
using Tasks.Write.Commands;

namespace Tasks.Write.CommandHandlers
{
    public class PrioritizeTaskHandler : ICommandHandler<PrioritizeTask>
    {
        private readonly IStoreEvents _storeEvents;

        public PrioritizeTaskHandler(IStoreEvents storeEvents)
        {
            _storeEvents = storeEvents;
        }

        public void Handle(PrioritizeTask command)
        {
            using (IEventStream stream = _storeEvents.OpenStream(command.MovedTaskId, 0, Int32.MaxValue))
            {
                var task = new Task();

                foreach (var committedEvent in stream.CommittedEvents)
                {
                    task.ApplyCommitted(committedEvent.Body);
                }

                task.PrioritizeTask(command.UserId, command.RelativeTaskId, command.TaskRelativePriority, command.UtcPrioritized);

                foreach (var uncommittedEvent in task.UncommittedEvents)
                {
                    stream.Add(new EventMessage { Body = uncommittedEvent });
                }

                stream.CommitChanges(Guid.NewGuid());
            }            
        }
    }
}