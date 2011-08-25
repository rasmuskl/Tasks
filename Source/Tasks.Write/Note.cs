using System;
using Tasks.Events;

namespace Tasks.Write
{
    public class Note : AggregateRoot
    {
        public void CreateNote(string title, string description, Guid noteId, Guid userId, DateTime utcCreated)
        {
            var evt = new NoteCreated(title, description, noteId, userId, utcCreated);
            Apply(evt);
            UncommittedEvents.Add(evt);
        }

        private void Apply(NoteCreated evt)
        {
        }
    }
}