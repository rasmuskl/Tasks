using System.Linq;
using System.Collections.Generic;
using System;
using Tasks.Read.Models;

namespace Tasks.Read.Queries
{
    public class QueryRecentlyCompletedTasksByContextId : IQuery<IEnumerable<TaskReadModel>>
    {
        public Guid UserId { get; set; }
        public Guid ContextId { get; set; }

        public QueryRecentlyCompletedTasksByContextId(Guid userId, Guid contextId)
        {
            UserId = userId;
            ContextId = contextId;
        }
    }
}