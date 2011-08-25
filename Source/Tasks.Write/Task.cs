using System;
using System.Collections.Generic;
using Tasks.Events;

namespace Tasks.Write
{
    public class Task : AggregateRoot
    {
        public void CreateTask(string title, Guid taskId, Guid userId, DateTime utcCreated)
        {
            var evt = new TaskCreated(title, taskId, userId, utcCreated);
            Apply(evt);
            UncommittedEvents.Add(evt);
        }

        private void Apply(TaskCreated evt)
        {
            
        }
    }
}