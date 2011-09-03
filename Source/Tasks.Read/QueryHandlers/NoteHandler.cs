using System.Collections.Generic;
using System.Linq;
using Tasks.Read.Models;
using Tasks.Read.Queries;

namespace Tasks.Read.QueryHandlers
{
    public class NoteHandler :
        IQueryHandler<QueryNotesByContextId, IEnumerable<NoteReadModel>>,
        IQueryHandler<QueryNoteById, NoteReadModel>
    {
        public IEnumerable<NoteReadModel> Handle(QueryNotesByContextId query)
        {
            if (!ReadStorage.Notes.ContainsKey(query.UserId))
                return new NoteReadModel[] { };

            return ReadStorage.Notes[query.UserId].Where(x => x.ContextId == query.ContextId);
        }

        public NoteReadModel Handle(QueryNoteById query)
        {
            if (!ReadStorage.Notes.ContainsKey(query.UserId))
                return null;

            return ReadStorage.Notes[query.UserId].FirstOrDefault(x => x.NoteId == query.NoteId);
        }
    }
}