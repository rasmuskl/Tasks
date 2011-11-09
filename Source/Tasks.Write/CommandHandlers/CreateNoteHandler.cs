using Tasks.Write.Commands;

namespace Tasks.Write.CommandHandlers
{
    public class CreateNoteHandler : ICommandHandler<CreateNote>
    {
        readonly IRepository _repository;

        public CreateNoteHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CreateNote command)
        {
            var note = _repository.Get<Note>(command.NoteId);
            note.CreateNote(command.Title, command.Description, command.NoteId, command.UserId, command.UtcCreated);
            _repository.Commit(command.NoteId, note);
        }
    }
}