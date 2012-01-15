using System;
using System.Collections.Generic;
using Tasks.Read.Models;

namespace Tasks.Read.Queries
{
    public class QueryFragmentsByUserId : IQuery<IEnumerable<FragmentReadModel>>
    {
        public Guid UserId { get; set; }

        public QueryFragmentsByUserId(Guid userId)
        {
            UserId = userId;
        }
    }
}