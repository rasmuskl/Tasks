using System;

namespace Tasks.Events
{
    public class NoteCreated
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid NoteId { get; set; }

        public NoteCreated(string title, string description, Guid noteId)
        {
            Title = title;
            Description = description;
            NoteId = noteId;
        }
    }
}