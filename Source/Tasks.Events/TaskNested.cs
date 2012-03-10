using System.Linq;
using System.Collections.Generic;
using System;

namespace Tasks.Events
{
    public class TaskNested
    {
        public Guid UserId { get; set; }
        public Guid TaskId { get; set; }
        public Guid ParentTaskId { get; set; }
        public DateTime UtcNested { get; set; }

        public TaskNested(Guid userId, Guid taskId, Guid parentTaskId, DateTime utcNested)
        {
            UserId = userId;
            TaskId = taskId;
            ParentTaskId = parentTaskId;
            UtcNested = utcNested;
        }
    }
}