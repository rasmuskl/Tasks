using System;
using Tasks.Events;

namespace Tasks.Write
{
    public class Task : AggregateRoot
    {
        private Guid _taskId;

        public void CreateTask(string title, Guid taskId, Guid userId, DateTime utcCreated)
        {
            ApplyUncommitted(new TaskCreated(title, taskId, userId, utcCreated));
        }

        public void CompleteTask(DateTime utcCompleted, Guid userId)
        {
            ApplyUncommitted(new TaskCompleted(utcCompleted, userId, _taskId));
        }

        private void Apply(TaskCreated evt)
        {
            _taskId = evt.TaskId;
        }

        private void Apply(TaskCompleted evt)
        {
            
        }
    }
}