using System;

namespace Tasks.Read.Queries
{
    public class QueryUserHasContextNamed : IQuery<bool>
    {
        public Guid UserId { get; set; }
        public string ContextName { get; set; }

        public QueryUserHasContextNamed(Guid userId, string contextName)
        {
            UserId = userId;
            ContextName = contextName;
        }
    }
}