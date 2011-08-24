using Tasks.Events;

namespace Tasks.Read.EventHandlers
{
    public class TaskCreatedHandler : IEventHandler<TaskCreated>
    {
        public void Handle(TaskCreated @event)
        {
            ReadStorage.Tasks.Add(@event.Title);
        }
    }
}