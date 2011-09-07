using System;

namespace Tasks.Events
{
    public class TaskPrioritized
    {
        public Guid UserId { get; set; }
        public Guid MovedTaskId { get; set; }
        public Guid RelativeTaskId { get; set; }
        public TaskRelativePriority MoveDirection { get; set; }
        public DateTime UtcTaskPrioritized { get; set; }

        public TaskPrioritized(Guid userId, Guid movedTaskId, Guid relativeTaskId, TaskRelativePriority moveDirection, DateTime utcTaskPrioritized)
        {
            UserId = userId;
            MovedTaskId = movedTaskId;
            RelativeTaskId = relativeTaskId;
            MoveDirection = moveDirection;
            UtcTaskPrioritized = utcTaskPrioritized;
        }
    }
}