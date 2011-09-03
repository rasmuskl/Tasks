using System;
using Machine.Specifications;
using Tasks.Events;
using System.Linq;
using Tasks.Write.Commands;

namespace Tasks.Tests.Write
{
    public class when_create_note_is_processed : WriteContext
    {
        static CreateNote _createNote;

        Establish context = () =>
            {
                _createNote = new CreateNote("title", "description", Guid.NewGuid());
            };

        Because of = () => _executor.Execute(_createNote);

        It should_publish_one_event = () => _eventsPublished.Count.ShouldEqual(1);

        It should_contain_a_create_note_event = () => _eventsPublished.Count(x => x is NoteCreated).ShouldEqual(1);
    }
}