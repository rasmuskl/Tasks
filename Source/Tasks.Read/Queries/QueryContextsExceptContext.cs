using System;
using System.Collections.Generic;
using Tasks.Read.Models;

namespace Tasks.Read.Queries
{
    public class QueryContextsExceptContext : IQuery<IEnumerable<ContextReadModel>>
    {
        public Guid UserId { get; set; }
        public Guid ContextId { get; set; }

        public QueryContextsExceptContext(Guid userId, Guid contextId)
        {
            UserId = userId;
            ContextId = contextId;
        }
    }
}