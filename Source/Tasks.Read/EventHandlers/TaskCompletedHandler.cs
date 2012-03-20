using System.Collections.Generic;
using Tasks.Events;
using Tasks.Read.Models;
using System.Linq;
using Tasks.Read.Queries;

namespace Tasks.Read.EventHandlers
{
    public class TaskCompletedHandler : IEventHandler<TaskCompleted>
    {
        public void Handle(TaskCompleted evt)
        {
            List<TaskReadModel> container = ReadStorage.Tasks[evt.UserId];

            TaskReadModel task = ReadStorage.Query(new QueryTaskById(evt.UserId, evt.TaskId));

            if (task == null)
            {
                return;
            }

            if (task.ParentTask != null)
            {
                container = task.ParentTask.NestedTasks;
            }

            task.UtcCompleted = evt.UtcCompleted;

            List<TaskReadModel> completedTasks;

            if (!ReadStorage.CompletedTasks.TryGetValue(evt.UserId, out completedTasks))
            {
                completedTasks = new List<TaskReadModel>();
                ReadStorage.CompletedTasks[evt.UserId] = completedTasks;
            }

            container.Remove(task);
            completedTasks.Add(task);
        }
    }
}