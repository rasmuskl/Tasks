using System;
using EventStore;
using Tasks.Write.Commands;

namespace Tasks.Write.CommandHandlers
{
    public class CreateFragmentHandler : ICommandHandler<CreateFragment>
    {
        readonly IStoreEvents _eventStore;

        public CreateFragmentHandler(IStoreEvents eventStore)
        {
            _eventStore = eventStore;
        }

        public void Handle(CreateFragment command)
        {
            using (var stream = _eventStore.CreateStream(command.FragmentId))
            {
                var fragment = new Fragment();

                fragment.CreateFragment(command.FragmentId, command.Text, command.UserId);

                foreach (var uncommittedEvent in fragment.UncommittedEvents)
                {
                    stream.Add(new EventMessage { Body = uncommittedEvent });
                }

                stream.CommitChanges(Guid.NewGuid());
            }
        }
    }
}