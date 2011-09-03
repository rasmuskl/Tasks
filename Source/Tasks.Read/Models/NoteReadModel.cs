using System;

namespace Tasks.Read.Models
{
    public class NoteReadModel
    {
        public Guid NoteId { get; set; }
        public string Title { get; set; }
        public string DescriptionRaw { get; set; }
        public string DescriptionHtml { get; set; }
        public Guid ContextId { get; set; }
    }
}