using System;
using Tasks.Events;

namespace Tasks.Write
{
    public class Task : AggregateRoot
    {
        private Guid _taskId;

        public void CreateTask(string title, Guid taskId, Guid userId, Guid contextId, DateTime utcCreated)
        {
            ApplyUncommitted(new TaskCreated(title, taskId, userId, contextId, utcCreated));
        }

        public void CompleteTask(DateTime utcCompleted, Guid userId)
        {
            ApplyUncommitted(new TaskCompleted(utcCompleted, userId, _taskId));
        }

        public void MoveToContext(Guid userId, Guid targetContextId)
        {
            ApplyUncommitted(new TaskMovedToContext(userId, _taskId, targetContextId));
        }

        public void PrioritizeTask(Guid userId, Guid relativeTaskId, TaskRelativePriority relativePriority, DateTime utcPrioritized)
        {
            ApplyUncommitted(new TaskPrioritized(userId, _taskId, relativeTaskId, relativePriority, utcPrioritized));
        }

        public void NestTask(Guid userId, Guid parentTaskId, DateTime utcNested)
        {
            ApplyUncommitted(new TaskNested(userId, _taskId, parentTaskId, utcNested));
        }

        private void Apply(TaskCreated evt)
        {
            _taskId = evt.TaskId;
        }

        private void Apply(TaskCompleted evt)
        {
            
        }

        private void Apply(TaskMovedToContext evt)
        {
            
        }

        private void Apply(TaskPrioritized evt)
        {
            
        }

        private void Apply(TaskNested evt)
        {
            
        }
    }
}