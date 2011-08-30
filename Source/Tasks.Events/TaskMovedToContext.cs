using System;

namespace Tasks.Events
{
    public class TaskMovedToContext
    {
        public Guid TargetContextId { get; set; }
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }

        public TaskMovedToContext(Guid userId, Guid taskId, Guid targetContextId)
        {
            TargetContextId = targetContextId;
            UserId = userId;
            TaskId = taskId;
        }
    }
}