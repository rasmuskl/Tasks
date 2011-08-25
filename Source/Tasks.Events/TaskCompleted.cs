using System;

namespace Tasks.Events
{
    public class TaskCompleted
    {
        public DateTime UtcCompleted { get; set; }

        public TaskCompleted(DateTime utcCompleted)
        {
            UtcCompleted = utcCompleted;
        }
    }
}