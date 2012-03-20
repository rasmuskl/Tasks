using System.Collections.Generic;
using Tasks.Events;
using System.Linq;
using Tasks.Read.Models;
using Tasks.Read.Queries;

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

            var movedTask = ReadStorage.Query(new QueryTaskById(evt.UserId, evt.MovedTaskId));
            var relativeTask = ReadStorage.Query(new QueryTaskById(evt.UserId, evt.RelativeTaskId));

            if(movedTask.ParentTask != relativeTask.ParentTask)
            {
                return;
            }

            List<TaskReadModel> container = ReadStorage.Tasks[evt.UserId];

            if(movedTask.ParentTask != null)
            {
                container = movedTask.ParentTask.NestedTasks;
            }

            var movedTaskIndex = container.FindIndex(x => x.TaskId == evt.MovedTaskId);
            var relativedTaskIndex = container.FindIndex(x => x.TaskId == evt.RelativeTaskId);

            if(movedTaskIndex == -1 || relativedTaskIndex == -1)
            {
                return;
            }

            container.RemoveAt(movedTaskIndex);

            relativedTaskIndex = container.FindIndex(x => x.TaskId == evt.RelativeTaskId);

            if(evt.MoveDirection == TaskRelativePriority.PrioritizedHigher)
            {
                container.Insert(relativedTaskIndex, movedTask);
            }
            else
            {
                container.Insert(relativedTaskIndex + 1, movedTask);
            }
        }
    }
}