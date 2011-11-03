using System.Collections.Generic;
using System.Linq;
using Tasks.Read.Models;
using Tasks.Read.Queries;

namespace Tasks.Read.QueryHandlers
{
    public class TaskHandler :
        IQueryHandler<QueryTasksByContextId, IEnumerable<TaskReadModel>>,
        IQueryHandler<QueryUserHasTask, bool>,
        IQueryHandler<QueryRecentlyCompletedTasksByContextId, IEnumerable<TaskReadModel>>,
        IQueryHandler<QueryRecentlyCompletedTasks, IEnumerable<TaskReadModel>>
    {
        public IEnumerable<TaskReadModel> Handle(QueryTasksByContextId query)
        {
            if (!ReadStorage.Tasks.ContainsKey(query.UserId))
                return new TaskReadModel[] { };

            return ReadStorage.Tasks[query.UserId].Where(x => x.ContextId == query.ContextId);
        }

        public bool Handle(QueryUserHasTask query)
        {
            if (!ReadStorage.Tasks.ContainsKey(query.UserId))
                return false;

            return ReadStorage.Tasks[query.UserId].Any(x => x.TaskId == query.TaskId);
        }

        public IEnumerable<TaskReadModel> Handle(QueryRecentlyCompletedTasksByContextId query)
        {
            if (!ReadStorage.CompletedTasks.ContainsKey(query.UserId))
                return new TaskReadModel[] {};

            return ReadStorage.CompletedTasks[query.UserId].Where(x => x.ContextId == query.ContextId).Reverse().ToArray();
        }

        public IEnumerable<TaskReadModel> Handle(QueryRecentlyCompletedTasks query)
        {
            if (!ReadStorage.CompletedTasks.ContainsKey(query.UserId))
                return new TaskReadModel[] { };

            return ReadStorage.CompletedTasks[query.UserId].AsEnumerable().Reverse().ToArray();
        }
    }
}