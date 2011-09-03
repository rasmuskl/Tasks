using MarkdownSharp;
using Tasks.Events;
using Tasks.Read.Queries;

namespace Tasks.Read.EventHandlers
{
    public class NoteDescriptionChangedHandler : IEventHandler<NoteDescriptionChanged>
    {
        public void Handle(NoteDescriptionChanged evt)
        {
            var note = ReadStorage.Query(new QueryNoteById(evt.UserId, evt.NoteId));

            note.DescriptionRaw = evt.NewDescription;
            note.DescriptionHtml = new Markdown().Transform(evt.NewDescription);
        }
    }
}