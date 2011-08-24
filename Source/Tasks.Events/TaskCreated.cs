using System;

namespace Tasks.Events
{
    public class TaskCreated
    {
        public Guid TaskId { get; set; }
        public string Title { get; set; }

        public TaskCreated(string title, Guid taskId)
        {
            Title = title;
            TaskId = taskId;
        }
    }
}