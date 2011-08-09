using System;
using System.Collections.Generic;
using Tasks.Model.Events;

namespace Tasks.Model
{
    public class Task
    {
        public Task()
        {
            UncommittedEvents = new List<object>();
        }

        public void CreateTask(string taskCaption, Guid taskId)
        {
            Apply(new TaskCreatedEvent(taskCaption, taskId));
        }

        private void Apply(TaskCreatedEvent evt)
        {
            UncommittedEvents.Add(evt);
        }

        public List<object> UncommittedEvents { get; private set; }
    }
}