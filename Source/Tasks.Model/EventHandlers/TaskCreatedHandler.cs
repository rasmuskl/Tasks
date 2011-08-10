using Tasks.Model.Events;

namespace Tasks.Model.EventHandlers
{
    public class TaskCreatedHandler : IEventHandler<TaskCreatedEvent>
    {
        public void Handle(TaskCreatedEvent @event)
        {
            Storage.Tasks.Add(@event.TaskCaption);
        }
    }
}