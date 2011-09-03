using System;
using System.Collections.Generic;
using Tasks.Read.Models;

namespace Tasks.Read.Queries
{
    public class QueryNotesByContextId : IQuery<IEnumerable<NoteReadModel>>
    {
        public Guid UserId { get; set; }
        public Guid ContextId { get; set; }

        public QueryNotesByContextId(Guid userId, Guid contextId)
        {
            UserId = userId;
            ContextId = contextId;
        }
    }
}