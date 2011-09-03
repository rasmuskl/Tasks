using System.Collections.Generic;
using System.Linq;
using Tasks.Read.Models;
using Tasks.Read.Queries;

namespace Tasks.Read.QueryHandlers
{
    public class NoteHandler :
        IQueryHandler<QueryNotesByContextId, IEnumerable<NoteReadModel>>
    {
        public IEnumerable<NoteReadModel> Handle(QueryNotesByContextId query)
        {
            if (!ReadStorage.Notes.ContainsKey(query.UserId))
                return new NoteReadModel[] { };

            return ReadStorage.Notes[query.UserId].Where(x => x.ContextId == query.ContextId);
        }
    }
}