using System;

namespace Tasks.Read.Models
{
    public class NoteReadModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid ContextId { get; set; }
    }
}