using System;
using System.Linq;
using System.Collections.Generic;
using Tasks.Events;
using Tasks.Read.Queries;

namespace Tasks.Read.EventHandlers
{
    public class TaskNestedHandler : IEventHandler<TaskNested>
    {
        public void Handle(TaskNested evt)
        {
            var task = ReadStorage.Query(new QueryTaskById(evt.UserId, evt.TaskId));
            var parentTask = ReadStorage.Query(new QueryTaskById(evt.UserId, evt.ParentTaskId));
            
            if(task.ParentTask == null)
            {
                ReadStorage.Tasks[evt.UserId].Remove(task);
            }
            else
            {
                task.ParentTask.NestedTasks.Remove(task);
            }

            parentTask.NestedTasks.Add(task); 
            task.ParentTask = parentTask;
        }
    }
}