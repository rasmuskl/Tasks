using System;

namespace Tasks.Read.Models
{
    public class TaskReadModel
    {
        public Guid TaskId { get; set; } 
        public string Title { get; set; }
        public Guid ContextId { get; set; }
        public DateTime UtcCompleted { get; set; }
    }
}