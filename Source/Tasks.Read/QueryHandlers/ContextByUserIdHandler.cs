using System;
using System.Collections.Generic;
using Tasks.Read.Models;
using Tasks.Read.Queries;

namespace Tasks.Read.QueryHandlers
{
    public class ContextByUserIdHandler : IQueryHandler<QueryContextsByUserId, List<ContextReadModel>>
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
    }
}