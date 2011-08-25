using System.Collections.Generic;
using Tasks.Events;

namespace Tasks.Read.EventHandlers
{
    public class TaskCompletedHandler : IEventHandler<TaskCompleted>
    {
        public void Handle(TaskCompleted evt)
        {
            List<string> tasks;

            if(ReadStorage.Tasks.TryGetValue(evt.UserId, out tasks))
            {
            }
        }
    }
}