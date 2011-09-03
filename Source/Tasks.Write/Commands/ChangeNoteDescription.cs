using System;

namespace Tasks.Write.Commands
{
    public class ChangeNoteDescription
    {
        public Guid UserId { get; set; }
        public Guid NoteId { get; set; }
        public string NewDescription { get; set; }

        public ChangeNoteDescription(Guid userId, Guid noteId, string newDescription)
        {
            UserId = userId;
            NoteId = noteId;
            NewDescription = newDescription;
        }
    }
}