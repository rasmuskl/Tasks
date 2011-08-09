using System;

namespace Tasks.Model.Events
{
    public class TaskCreatedEvent
    {
        public Guid TaskId { get; set; }
        public string TaskCaption { get; set; }

        public TaskCreatedEvent(string taskCaption, Guid taskId)
        {
            TaskCaption = taskCaption;
            TaskId = taskId;
        }
    }
}