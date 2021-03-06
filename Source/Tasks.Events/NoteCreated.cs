using System;

namespace Tasks.Events
{
    public class NoteCreated
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid NoteId { get; set; }
        public Guid UserId { get; set; }
        public DateTime UtcCreated { get; set; }

        public NoteCreated(string title, string description, Guid noteId, Guid userId, DateTime utcCreated)
        {
            Title = title;
            Description = description;
            NoteId = noteId;
            UserId = userId;
            UtcCreated = utcCreated;
        }
    }
}