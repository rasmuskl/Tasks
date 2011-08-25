using System;

namespace Tasks.Events
{
    public class TaskCompleted
    {
        public DateTime UtcCompleted { get; set; }
        public Guid UserId { get; set; }
        public Guid TaskId { get; set; }

        public TaskCompleted(DateTime utcCompleted, Guid userId, Guid taskId)
        {
            UtcCompleted = utcCompleted;
            UserId = userId;
            TaskId = taskId;
        }
    }
}