using System;

namespace Tasks.Write.Commands
{
    public class CreateNote
    {
        public CreateNote(string title, string description, Guid userId)
        {
            Title = title;
            Description = description;
            UserId = userId;
            UtcCreated = DateTime.UtcNow;
            NoteId = Guid.NewGuid();
        }

        public Guid NoteId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime UtcCreated { get; private set; }
    }
}