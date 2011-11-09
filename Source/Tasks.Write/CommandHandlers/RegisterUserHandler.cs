using Tasks.Write.Commands;

namespace Tasks.Write.CommandHandlers
{
    public class RegisterUserHandler : ICommandHandler<RegisterUser>
    {
        readonly IRepository _repository;

        public RegisterUserHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(RegisterUser command)
        {
            var user = _repository.Get<User>(command.UserId);
            user.RegisterUser(command.UserId, command.Email, command.PasswordSha1);
            _repository.Commit(command.UserId, user);
        }
    }
}