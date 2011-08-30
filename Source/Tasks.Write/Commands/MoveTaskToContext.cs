using System;

namespace Tasks.Write.Commands
{
    public class MoveTaskToContext
    {
        public Guid TaskId { get; private set; }
        public Guid UserId { get; private set; }
        public Guid TargetContextId { get; private set; }

        public MoveTaskToContext(Guid taskId, Guid userId, Guid targetContextId)
        {
            TaskId = taskId;
            UserId = userId;
            TargetContextId = targetContextId;
        }
    }
}