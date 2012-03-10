using System;
using System.Collections.Generic;

namespace Tasks.Read.Models
{
    public class TaskReadModel
    {
        public TaskReadModel()
        {
            NestedTasks = new List<TaskReadModel>();
        }

        public Guid TaskId { get; set; } 
        public string Title { get; set; }
        public Guid ContextId { get; set; }
        public DateTime UtcCompleted { get; set; }

        public TaskReadModel ParentTask { get; set; }
        public List<TaskReadModel> NestedTasks { get; set; }
    }
}