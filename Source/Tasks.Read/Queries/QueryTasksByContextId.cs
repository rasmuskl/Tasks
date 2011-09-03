using System;
using System.Collections.Generic;
using Tasks.Read.Models;

namespace Tasks.Read.Queries
{
    public class QueryTasksByContextId : IQuery<IEnumerable<TaskReadModel>>
    {
        public Guid UserId { get; set; }
        public Guid ContextId { get; set; }

        public QueryTasksByContextId(Guid userId, Guid contextId)
        {
            UserId = userId;
            ContextId = contextId;
        }
    }
}