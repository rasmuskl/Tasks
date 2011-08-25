using System;

namespace Tasks.Write.Commands
{
    public class CompleteTask
    {
        public Guid TaskId { get; set; }
        public DateTime UtcCompleted { get; set; }
        public Guid UserId { get; set; }

        public CompleteTask(Guid taskId, Guid userId)
        {
            TaskId = taskId;
            UserId = userId;
            UtcCompleted = DateTime.UtcNow;
        }
    }
}