using System;
using Tasks.Read.Models;

namespace Tasks.Read.Queries
{
    public class QueryNoteById : IQuery<NoteReadModel>
    {
        public Guid UserId { get; set; }
        public Guid NoteId { get; set; }

        public QueryNoteById(Guid userId, Guid noteId)
        {
            UserId = userId;
            NoteId = noteId;
        }
    }
}