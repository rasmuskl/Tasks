using System;
using Machine.Specifications;
using Tasks.Events;
using Tasks.Write.Commands;
using System.Linq;

namespace Tasks.Tests.Write
{
    public class when_note_description_is_changed : WriteContext
    {
        static ChangeNoteDescription _command;

        Establish context = () =>
            {
                var userRegistered = new UserRegistered(Guid.NewGuid(), "note-desc-test@test.dk", "1234");
                var noteCreated = new NoteCreated("note", "desc", Guid.NewGuid(), userRegistered.UserId, DateTime.Now);

                AddToHistory(userRegistered.UserId, userRegistered);
                AddToHistory(noteCreated.NoteId, noteCreated);

                _command = new ChangeNoteDescription(userRegistered.UserId, noteCreated.NoteId, "new desc");
            };

        Because of = () => _executor.Execute(_command);

        It should_public_one_event = () => _eventsPublished.Count.ShouldEqual(1);

        It should_publish_a_note_desc_changed_event = () =>
            _eventsPublished.OfType<NoteDescriptionChanged>().Count().ShouldEqual(1);
    }
}