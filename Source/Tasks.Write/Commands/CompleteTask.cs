using System;

namespace Tasks.Write.Commands
{
    public class CompleteTask
    {
        public Guid TaskId { get; set; }
        public DateTime UtcCompleted { get; set; }

        public CompleteTask(Guid taskId)
        {
            TaskId = taskId;
            UtcCompleted = DateTime.UtcNow;
        }
    }
}