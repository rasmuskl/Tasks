using System;

namespace Tasks.Events
{
    public class TaskCreated
    {
        public Guid TaskId { get; set; }
        public string TaskCaption { get; set; }

        public TaskCreated(string taskCaption, Guid taskId)
        {
            TaskCaption = taskCaption;
            TaskId = taskId;
        }
    }
}