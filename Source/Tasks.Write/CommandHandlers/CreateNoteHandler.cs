using System;
using EventStore;
using Tasks.Write.Commands;

namespace Tasks.Write.CommandHandlers
{
    public class CreateNoteHandler : ICommandHandler<CreateNote>
    {
        private readonly IStoreEvents _eventStore;

        public CreateNoteHandler(IStoreEvents eventStore)
        {
            _eventStore = eventStore;
        }

        public void Handle(CreateNote command)
        {
            using(var stream = _eventStore.CreateStream(command.NoteId))
            {
                var note = new Note();

                note.CreateNote(command.Title, command.Description, command.NoteId, command.UserId, command.UtcCreated);

                foreach (var uncommittedEvent in note.UncommittedEvents)
                {
                    stream.Add(new EventMessage { Body = uncommittedEvent });
                }

                stream.CommitChanges(Guid.NewGuid());
            }
        }
    }
}