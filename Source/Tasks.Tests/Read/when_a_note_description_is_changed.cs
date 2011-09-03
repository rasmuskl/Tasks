using System;
using Machine.Specifications;
using Tasks.Events;
using Tasks.Read;
using Tasks.Read.Queries;

namespace Tasks.Tests.Read
{
    public class when_a_note_description_is_changed : ReadContext
    {
        static NoteDescriptionChanged _evt;

        Establish context = () =>
            {
                var noteCreated = ProcessedEvent(new NoteCreated("note", "desc", Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow));

                _evt = new NoteDescriptionChanged(noteCreated.UserId, noteCreated.NoteId, "**new desc**");
            };

        Because of = () => ProcessedEvent(_evt);

        It should_have_an_updated_description = () =>
            ReadStorage.Query(new QueryNoteById(_evt.UserId, _evt.NoteId)).DescriptionRaw.ShouldEqual(_evt.NewDescription);

        It should_have_an_updated_html_description = () =>
            ReadStorage.Query(new QueryNoteById(_evt.UserId, _evt.NoteId)).DescriptionHtml.Contains("<strong>").ShouldBeTrue();
    }
}