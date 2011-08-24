using System;

namespace Tasks.Write.Commands
{
    public class CreateNote
    {
        public CreateNote(string title, string description)
        {
            Title = title;
            Description = description;
            NoteId = Guid.NewGuid();
        }

        public Guid NoteId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
    }
}