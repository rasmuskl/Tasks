using System;
using Tasks.Events;

namespace Tasks.Write
{
    public class Note : AggregateRoot
    {
        private Guid _noteId;

        public void CreateNote(string title, string description, Guid noteId, Guid userId, DateTime utcCreated)
        {
            ApplyUncommitted(new NoteCreated(title, description, noteId, userId, utcCreated));
        }

        public void ChangeDescription(Guid userId, string newDescription)
        {
            ApplyUncommitted(new NoteDescriptionChanged(userId, _noteId, newDescription));
        }

        private void Apply(NoteCreated evt)
        {
            _noteId = evt.NoteId;
        }

        private void Apply(NoteDescriptionChanged evt)
        {
            
        }
    }
}