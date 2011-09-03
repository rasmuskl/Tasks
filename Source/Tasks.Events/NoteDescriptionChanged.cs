using System;

namespace Tasks.Events
{
    public class NoteDescriptionChanged
    {
        public Guid UserId { get; set; }
        public Guid NoteId { get; set; }
        public string NewDescription { get; set; }

        public NoteDescriptionChanged(Guid userId, Guid noteId, string newDescription)
        {
            UserId = userId;
            NoteId = noteId;
            NewDescription = newDescription;
        }
    }
}