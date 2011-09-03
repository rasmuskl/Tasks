using System;
using System.Collections.Generic;
using Tasks.Read.Models;
using Tasks.Read.Queries;
using System.Linq;

namespace Tasks.Read.QueryHandlers
{
    public class ContextHandler : 
        IQueryHandler<QueryContextsByUserId, List<ContextReadModel>>,
        IQueryHandler<QueryUserHasContextNamed, bool>,
        IQueryHandler<QueryContextById, ContextReadModel>
    {
        public List<ContextReadModel> Handle(QueryContextsByUserId query)
        {
            var list = new List<ContextReadModel>
                {
                    new ContextReadModel {ContextId = Guid.Empty, Name = "General"}
                };

            List<ContextReadModel> otherContexts;
            if (ReadStorage.Contexts.TryGetValue(query.UserId, out otherContexts))
            {
                list.AddRange(otherContexts);
            }

            return list;
        }

        public bool Handle(QueryUserHasContextNamed query)
        {
            return ReadStorage.Query(new QueryContextsByUserId(query.UserId))
                .Any(x => string.Equals(x.Name, query.ContextName, StringComparison.InvariantCultureIgnoreCase));
        }

        public ContextReadModel Handle(QueryContextById query)
        {
            return ReadStorage.Query(new QueryContextsByUserId(query.UserId)).FirstOrDefault(x => x.ContextId == query.ContextId);
        }
    }
}