using System;
using System.Collections.Generic;
using Tasks.Events;

namespace Tasks.Write
{
    public class Task
    {
        public Task()
        {
            UncommittedEvents = new List<object>();
        }

        public void CreateTask(string title, Guid taskId, Guid userId, DateTime utcCreated)
        {
            Apply(new TaskCreated(title, taskId, userId, utcCreated));
        }

        private void Apply(TaskCreated evt)
        {
            UncommittedEvents.Add(evt);
        }

        public List<object> UncommittedEvents { get; private set; }
    }
}