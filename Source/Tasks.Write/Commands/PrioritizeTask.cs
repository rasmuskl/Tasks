using System;
using Tasks.Events;

namespace Tasks.Write.Commands
{
    public class PrioritizeTask
    {
        public Guid UserId { get; set; }
        public Guid MovedTaskId { get; set; }
        public Guid RelativeTaskId { get; set; }
        public TaskRelativePriority TaskRelativePriority { get; set; }
        public DateTime UtcPrioritized { get; set; }

        public PrioritizeTask(Guid userId, Guid movedTaskId, Guid relativeTaskId, TaskRelativePriority taskRelativePriority, DateTime prioritizedUtc)
        {
            UserId = userId;
            MovedTaskId = movedTaskId;
            RelativeTaskId = relativeTaskId;
            TaskRelativePriority = taskRelativePriority;
            UtcPrioritized = prioritizedUtc;
        }
    }
}