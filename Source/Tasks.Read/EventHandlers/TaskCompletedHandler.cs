using System.Collections.Generic;
using Tasks.Events;
using Tasks.Read.Models;

namespace Tasks.Read.EventHandlers
{
    public class TaskCompletedHandler : IEventHandler<TaskCompleted>
    {
        public void Handle(TaskCompleted evt)
        {
            List<TaskReadModel> tasks;

            if(ReadStorage.Tasks.TryGetValue(evt.UserId, out tasks))
            {
                tasks.RemoveAll(x => x.TaskId == evt.TaskId);
            }
        }
    }
}