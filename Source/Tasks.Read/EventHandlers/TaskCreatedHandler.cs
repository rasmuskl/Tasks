using System.Collections.Generic;
using Tasks.Events;

namespace Tasks.Read.EventHandlers
{
    public class TaskCreatedHandler : IEventHandler<TaskCreated>
    {
        public void Handle(TaskCreated @event)
        {
            if (!ReadStorage.Tasks.ContainsKey(@event.UserId))
            {
                ReadStorage.Tasks[@event.UserId] = new List<string>();
            }

            ReadStorage.Tasks[@event.UserId].Add(@event.Title);
        }
    }
}