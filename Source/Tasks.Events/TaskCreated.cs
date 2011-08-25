using System;

namespace Tasks.Events
{
    public class TaskCreated
    {
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }

        public TaskCreated(string title, Guid taskId, Guid userId)
        {
            Title = title;
            TaskId = taskId;
            UserId = userId;
        }
    }
}