using System;
using System.Linq;
using System.Collections.Generic;
using Tasks.Read.Models;

namespace Tasks.Read.Queries
{
    public class QueryTaskById : IQuery<TaskReadModel>
    {
        public Guid UserId { get; set; }
        public Guid TaskId { get; set; }

        public QueryTaskById(Guid userId, Guid taskId)
        {
            UserId = userId;
            TaskId = taskId;
        }

        public class Handler : IQueryHandler<QueryTaskById, TaskReadModel>
        {
            public TaskReadModel Handle(QueryTaskById query)
            {
                List<TaskReadModel> tasks;
                
                if (!ReadStorage.Tasks.TryGetValue(query.UserId, out tasks))
                    return null;

                return FindTaskRecursive(tasks, query.TaskId);
            }

            private TaskReadModel FindTaskRecursive(IEnumerable<TaskReadModel> tasks, Guid taskId)
            {
                foreach (var task in tasks)
                {
                    if (task.TaskId == taskId)
                        return task;

                    var nestedTask = FindTaskRecursive(task.NestedTasks, taskId);

                    if (nestedTask != null)
                        return nestedTask;
                }

                return null;
            }
        }
    }
}