using System;
using System.Collections.Generic;
using Tasks.Events;

namespace Tasks.Write
{
    public class Note
    {
        public Note()
        {
            UncommittedEvents = new List<object>();
        }

        public void CreateNote(string title, string description, Guid noteId, Guid userId)
        {
            Apply(new NoteCreated(title, description, noteId, userId));
        }

        private void Apply(NoteCreated evt)
        {
            UncommittedEvents.Add(evt);
        }

        public List<object> UncommittedEvents { get; private set; }
    }
}