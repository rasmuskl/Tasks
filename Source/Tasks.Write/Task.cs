using System;
using Tasks.Events;

namespace Tasks.Write
{
    public class Task : AggregateRoot
    {
        public void CreateTask(string title, Guid taskId, Guid userId, DateTime utcCreated)
        {
            ApplyUncommitted(new TaskCreated(title, taskId, userId, utcCreated));
        }

        public void CompleteTask(DateTime utcCompleted, Guid userId)
        {
            ApplyUncommitted(new TaskCompleted(utcCompleted, userId));
        }

        private void Apply(TaskCreated evt)
        {
            
        }

        private void Apply(TaskCompleted evt)
        {
            
        }
    }
}