using System.Collections.Generic;
using Tasks.Read.Models;
using Tasks.Read.Queries;
using System.Linq;

namespace Tasks.Read.QueryHandlers
{
    public class FragmentHandler : IQueryHandler<QueryFragmentsByUserId, IEnumerable<FragmentReadModel>>
    {
        public IEnumerable<FragmentReadModel> Handle(QueryFragmentsByUserId query)
        {
            List<FragmentReadModel> fragments;
            
            if(!ReadStorage.Fragments.TryGetValue(query.UserId, out fragments))
                return new FragmentReadModel[] {};

            return fragments.OrderByDescending(x => x.UtcCreated).ToArray();
        }
    }
}