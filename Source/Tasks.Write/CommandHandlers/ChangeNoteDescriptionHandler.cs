using Tasks.Write.Commands;

namespace Tasks.Write.CommandHandlers
{
    public class ChangeNoteDescriptionHandler : ICommandHandler<ChangeNoteDescription>
    {
        readonly IRepository _repository;

        public ChangeNoteDescriptionHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(ChangeNoteDescription command)
        {
            var note = _repository.Get<Note>(command.NoteId);
            note.ChangeDescription(command.UserId, command.NewDescription);
            _repository.Commit(command.NoteId, note);
        }
    }
}