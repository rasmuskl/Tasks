using System;
using Machine.Specifications;
using Tasks.Events;
using Tasks.Read;
using Tasks.Read.Queries;

namespace Tasks.Tests.Read
{
    public class when_a_note_is_created : ReadContext
    {
        static NoteCreated _noteCreated;

        Establish context = () =>
            {
                var userRegistered = WithEvent(new UserRegistered(Guid.NewGuid(), "note-test@test.dk", "1234"));

                _noteCreated = new NoteCreated("Task 1", "Note desc", Guid.NewGuid(), userRegistered.UserId, DateTime.Now);
            };

        Because of = () => WithEvent(_noteCreated);

        It should_find_note_in_general_context = () =>
            ReadStorage.Query(new QueryNotesByContextId(_noteCreated.UserId, Guid.Empty))
            .ShouldContain(x => x.NoteId == _noteCreated.NoteId);

        It should_not_find_note_in_another_context = () =>
            ReadStorage.Query(new QueryNotesByContextId(_noteCreated.UserId, Guid.NewGuid()))
            .ShouldNotContain(x => x.NoteId == _noteCreated.NoteId);
    }
}