using System.Collections.Generic;
using Tasks.Events;
using Tasks.Read.Models;

namespace Tasks.Read.EventHandlers
{
    public class TaskCreatedHandler : IEventHandler<TaskCreated>
    {
        public void Handle(TaskCreated evt)
        {
            if (!ReadStorage.Tasks.ContainsKey(evt.UserId))
            {
                ReadStorage.Tasks[evt.UserId] = new List<TaskReadModel>();
            }

            ReadStorage.Tasks[evt.UserId].Add(new TaskReadModel { TaskId = evt.TaskId, Title = evt.Title });
        }
    }
}