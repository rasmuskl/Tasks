using System;

namespace Tasks.Events
{
    public class TaskCreated
    {
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }
        public DateTime UtcCreated { get; set; }
        public string Title { get; set; }

        public TaskCreated(string title, Guid taskId, Guid userId, DateTime utcCreated)
        {
            Title = title;
            TaskId = taskId;
            UserId = userId;
            UtcCreated = utcCreated;
        }
    }
}