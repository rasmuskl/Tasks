using Tasks.Write.Commands;

namespace Tasks.Write.CommandHandlers
{
    public class CreateFragmentHandler : ICommandHandler<CreateFragment>
    {
        readonly IRepository _repository;

        public CreateFragmentHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CreateFragment command)
        {
            var fragment = _repository.Get<Fragment>(command.FragmentId);
            fragment.CreateFragment(command.FragmentId, command.Text, command.UserId);
            _repository.Commit(command.FragmentId, fragment);
        }
    }
}