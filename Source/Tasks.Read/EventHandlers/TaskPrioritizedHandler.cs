using System.Collections.Generic;
using Tasks.Events;
using Tasks.Read.Models;

namespace Tasks.Read.EventHandlers
{
    public class TaskPrioritizedHandler : IEventHandler<TaskPrioritized>
    {
        public void Handle(TaskPrioritized evt)
        {
            if (!ReadStorage.Tasks.ContainsKey(evt.UserId))
            {
                return;
            }

            var tasks = ReadStorage.Tasks[evt.UserId];

            var movedTaskIndex = tasks.FindIndex(x => x.TaskId == evt.MovedTaskId);
            var relativedTaskIndex = tasks.FindIndex(x => x.TaskId == evt.RelativeTaskId);

            if(movedTaskIndex == -1 || relativedTaskIndex == -1)
            {
                return;
            }

            var movedTask = tasks[movedTaskIndex];
            tasks.RemoveAt(movedTaskIndex);

            relativedTaskIndex = tasks.FindIndex(x => x.TaskId == evt.RelativeTaskId);

            if(evt.MoveDirection == TaskRelativePriority.PrioritizedHigher)
            {
                tasks.Insert(relativedTaskIndex, movedTask);
            }
            else
            {
                tasks.Insert(relativedTaskIndex + 1, movedTask);
            }
        }
    }
}