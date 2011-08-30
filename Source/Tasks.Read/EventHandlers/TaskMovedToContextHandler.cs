using System.Collections.Generic;
using Tasks.Events;
using Tasks.Read.Models;
using System.Linq;

namespace Tasks.Read.EventHandlers
{
    public class TaskMovedToContextHandler : IEventHandler<TaskMovedToContext>
    {
        public void Handle(TaskMovedToContext evt)
        {
            List<TaskReadModel> tasks;
            
            if(!ReadStorage.Tasks.TryGetValue(evt.UserId, out tasks))
                tasks = new List<TaskReadModel>();

            foreach (var task in tasks.Where(x => x.TaskId == evt.TaskId))
            {
                task.ContextId = evt.TargetContextId;
            }
        }
    }
}