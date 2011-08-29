using System;
using System.Collections.Generic;
using Tasks.Read.Models;

namespace Tasks.Read.Queries
{
    internal class QueryContextsByUserId : IQuery<List<ContextReadModel>>
    {
        readonly Guid _userId;

        public QueryContextsByUserId(Guid userId)
        {
            _userId = userId;
        }

        public List<ContextReadModel> Query()
        {
            var list = new List<ContextReadModel>
                {
                    new ContextReadModel {ContextId = Guid.Empty, Name = "General"}
                };

            List<ContextReadModel> otherContexts;
            if(ReadStorage.Contexts.TryGetValue(_userId, out otherContexts))
            {
                list.AddRange(otherContexts);
            }

            return list;
        }
    }
}