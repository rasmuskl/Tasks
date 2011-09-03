using System.Collections.Generic;
using System.Linq;
using Tasks.Read.Models;
using Tasks.Read.Queries;

namespace Tasks.Read.QueryHandlers
{
    public class TaskHandler :
        IQueryHandler<QueryTasksByContextId, IEnumerable<TaskReadModel>>
    {
        public IEnumerable<TaskReadModel> Handle(QueryTasksByContextId query)
        {
            if (!ReadStorage.Tasks.ContainsKey(query.UserId))
                return new TaskReadModel[] { };

            return ReadStorage.Tasks[query.UserId].Where(x => x.ContextId == query.ContextId);
        }
    }
}