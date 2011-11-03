using System.Collections.Generic;
using Tasks.Events;
using Tasks.Read.Models;
using System.Linq;

namespace Tasks.Read.EventHandlers
{
    public class TaskCompletedHandler : IEventHandler<TaskCompleted>
    {
        public void Handle(TaskCompleted evt)
        {
            List<TaskReadModel> tasks;

            if(ReadStorage.Tasks.TryGetValue(evt.UserId, out tasks))
            {
                TaskReadModel task = tasks.FirstOrDefault(x => x.TaskId == evt.TaskId);

                if(task == null)
                {
                    return;
                }

                List<TaskReadModel> completedTasks;

                if (!ReadStorage.CompletedTasks.TryGetValue(evt.UserId, out completedTasks))
                {
                    completedTasks = new List<TaskReadModel>();
                    ReadStorage.CompletedTasks[evt.UserId] = completedTasks;
                }

                tasks.Remove(task);
                completedTasks.Add(task);
            }
        }
    }
}