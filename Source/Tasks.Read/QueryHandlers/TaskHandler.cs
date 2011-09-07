using System.Collections.Generic;
using System.Linq;
using Tasks.Read.Models;
using Tasks.Read.Queries;

namespace Tasks.Read.QueryHandlers
{
    public class TaskHandler :
        IQueryHandler<QueryTasksByContextId, IEnumerable<TaskReadModel>>,
        IQueryHandler<QueryUserHasTask, bool>
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
    }
}