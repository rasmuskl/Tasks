using System;

namespace Tasks.Write.Commands
{
    public class CreateTask
    {
        public CreateTask(string title, Guid userId)
        {
            Title = title;
            UserId = userId;
            TaskId = Guid.NewGuid();
            UtcCreated = DateTime.UtcNow;
        }
        public CreateTask(string title, Guid userId, Guid contextId)
        {
            Title = title;
            UserId = userId;
            ContextId = contextId;
            TaskId = Guid.NewGuid();
            UtcCreated = DateTime.UtcNow;
        }

        public Guid TaskId { get; private set; }
        public string Title { get; private set; }
        public Guid UserId { get; private set; }
        public Guid ContextId { get; private set; }
        public DateTime UtcCreated { get; private set; }
    }
}