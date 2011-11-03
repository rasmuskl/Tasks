using System.Linq;
using System.Collections.Generic;
using System;
using Tasks.Read.Models;

namespace Tasks.Read.Queries
{
    public class QueryRecentlyCompletedTasks : IQuery<IEnumerable<TaskReadModel>>
    {
        public Guid UserId { get; set; }

        public QueryRecentlyCompletedTasks(Guid userId)
        {
            UserId = userId;
        }
    }
}