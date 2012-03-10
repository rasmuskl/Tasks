using System;
using System.Linq;
using System.Collections.Generic;
using Tasks.Events;
using Tasks.Read.Models;
using Tasks.Read.Queries;

namespace Tasks.Read.EventHandlers
{
    public class TaskNestedHandler : IEventHandler<TaskNested>
    {
        public void Handle(TaskNested evt)
        {
            var task = ReadStorage.Query(new QueryTaskById(evt.UserId, evt.TaskId));
            var parentTask = ReadStorage.Query(new QueryTaskById(evt.UserId, evt.ParentTaskId));
            
            RemoveTaskFromOldParent(evt, task);
            AddTaskToNewParent(evt, task, parentTask);
        }

        private static void AddTaskToNewParent(TaskNested evt, TaskReadModel task, TaskReadModel parentTask)
        {
            // Nested under new parent
            if (parentTask != null)
            {
                parentTask.NestedTasks.Add(task);
                task.ParentTask = parentTask;
            }
            else // Unnested to root
            {
                var oldParentTask = task.ParentTask;

                while(oldParentTask.ParentTask != null)
                {
                    oldParentTask = oldParentTask.ParentTask;
                }

                var rootTasks = ReadStorage.Tasks[evt.UserId];

                var oldParentRootIndex = rootTasks.IndexOf(oldParentTask);
                rootTasks.Insert(oldParentRootIndex + 1, task);

                task.ParentTask = null;
            }
        }

        private static void RemoveTaskFromOldParent(TaskNested evt, TaskReadModel task)
        {
            if (task.ParentTask == null)
            {
                ReadStorage.Tasks[evt.UserId].Remove(task);
            }
            else
            {
                task.ParentTask.NestedTasks.Remove(task);
            }
        }
    }
}