using Tasks.Write.Commands;

namespace Tasks.Write.CommandHandlers
{
    public class CreateContextHandler : ICommandHandler<CreateContext>
    {
        readonly IRepository _repository;

        public CreateContextHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CreateContext command)
        {
            var context = _repository.Get<Context>(command.ContextId);
            context.CreateContext(command.ContextId, command.ContextName, command.UserId, command.UtcCompleted);
            _repository.Commit(command.ContextId, context);
        }
    }
}