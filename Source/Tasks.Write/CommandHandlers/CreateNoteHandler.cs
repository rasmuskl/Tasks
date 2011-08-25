using System;
using System.Collections;
using EventStore;
using Tasks.Write.Commands;

namespace Tasks.Write.CommandHandlers
{
    public class CreateNoteHandler : IHandle<CreateNote>
    {
        public void Handle(CreateNote command)
        {
            using(IEventStream stream = Storage.Store.CreateStream(command.NoteId))
            {
                var note = new Note();

                note.CreateNote(command.Title, command.Description, command.NoteId, command.UserId);

                foreach (var uncommittedEvent in note.UncommittedEvents)
                {
                    stream.Add(new EventMessage { Body = uncommittedEvent });
                }

                stream.CommitChanges(Guid.NewGuid());
            }
        }
    }
}