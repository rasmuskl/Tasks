using System;

namespace Tasks.Read.Queries
{
    public class QueryContextIdByName : IQuery<Guid>
    {
        public QueryContextIdByName(Guid userId, string contextName)
        {
            UserId = userId;
            ContextName = contextName;
        }

        public Guid UserId { get; set; }
        public string ContextName { get; set; }
    }
}