using System.Collections.Generic;
using Tasks.Events;

namespace Tasks.Read.EventHandlers
{
    public class TaskCreatedHandler : IEventHandler<TaskCreated>
    {
        public void Handle(TaskCreated evt)
        {
            if (!ReadStorage.Tasks.ContainsKey(evt.UserId))
            {
                ReadStorage.Tasks[evt.UserId] = new List<string>();
            }

            ReadStorage.Tasks[evt.UserId].Add(evt.Title);
        }
    }
}