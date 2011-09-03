using System;
using Tasks.Read.Models;

namespace Tasks.Read.Queries
{
    public class QueryContextById : IQuery<ContextReadModel>
    {
        public Guid UserId { get; set; }
        public Guid ContextId { get; set; }

        public QueryContextById(Guid userId, Guid contextId)
        {
            UserId = userId;
            ContextId = contextId;
        }
    }
}