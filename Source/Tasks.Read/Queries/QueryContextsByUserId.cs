using System;
using System.Collections.Generic;
using Tasks.Read.Models;

namespace Tasks.Read.Queries
{
    public class QueryContextsByUserId : IQuery<List<ContextReadModel>>
    {
        public QueryContextsByUserId(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}