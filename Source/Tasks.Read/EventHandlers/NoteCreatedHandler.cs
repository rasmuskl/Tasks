using System;
using Tasks.Events;

namespace Tasks.Read.EventHandlers
{
    public class NoteCreatedHandler : IEventHandler<NoteCreated>
    {
        public void Handle(NoteCreated @event)
        {
            ReadStorage.Notes.Add(new Tuple<string, string>(@event.Title, @event.Description));
        }
    }
}