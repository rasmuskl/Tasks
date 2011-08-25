using System;

namespace Tasks.Events
{
    public class TaskCompleted
    {
        public DateTime UtcCompleted { get; set; }
        public Guid UserId { get; set; }

        public TaskCompleted(DateTime utcCompleted, Guid userId)
        {
            UtcCompleted = utcCompleted;
            UserId = userId;
        }
    }
}