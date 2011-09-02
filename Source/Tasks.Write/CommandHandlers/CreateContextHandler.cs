using System;
using EventStore;
using Tasks.Write.Commands;

namespace Tasks.Write.CommandHandlers
{
    public class CreateContextHandler : ICommandHandler<CreateContext>
    {
        readonly IStoreEvents _eventStore;

        public CreateContextHandler(IStoreEvents eventStore)
        {
            _eventStore = eventStore;
        }

        public void Handle(CreateContext command)
        {
            using (var stream = _eventStore.CreateStream(command.ContextId))
            {
                var context = new Context();

                context.CreateContext(command.ContextId, command.ContextName, command.UserId, command.UtcCompleted);

                foreach (var uncommittedEvent in context.UncommittedEvents)
                {
                    stream.Add(new EventMessage { Body = uncommittedEvent });
                }

                stream.CommitChanges(Guid.NewGuid());
            }
        }
    }
}