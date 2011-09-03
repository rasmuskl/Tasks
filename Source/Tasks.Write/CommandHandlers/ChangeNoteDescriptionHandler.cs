using System;
using EventStore;
using Tasks.Write.Commands;

namespace Tasks.Write.CommandHandlers
{
    public class ChangeNoteDescriptionHandler : ICommandHandler<ChangeNoteDescription>
    {
        private readonly IStoreEvents _storeEvents;

        public ChangeNoteDescriptionHandler(IStoreEvents storeEvents)
        {
            _storeEvents = storeEvents;
        }

        public void Handle(ChangeNoteDescription command)
        {
            using (var stream = _storeEvents.OpenStream(command.NoteId, 0, Int32.MaxValue))
            {
                var note = new Note();

                foreach (var committedEvent in stream.CommittedEvents)
                {
                    note.ApplyCommitted(committedEvent.Body);
                }

                note.ChangeDescription(command.UserId, command.NewDescription);

                foreach (var uncommittedEvent in note.UncommittedEvents)
                {
                    stream.Add(new EventMessage { Body = uncommittedEvent });
                }
                
                stream.CommitChanges(Guid.NewGuid());
            }

            
        }
    }
}