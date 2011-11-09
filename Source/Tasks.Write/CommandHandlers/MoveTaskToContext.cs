using Tasks.Write.Commands;

namespace Tasks.Write.CommandHandlers
{
    public class MoveTaskToContextHandler : ICommandHandler<MoveTaskToContext>
    {
        readonly IRepository _repository;

        public MoveTaskToContextHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(MoveTaskToContext command)
        {
            var task = _repository.Get<Task>(command.TaskId);
            task.MoveToContext(command.UserId, command.TargetContextId);
            _repository.Commit(command.TaskId, task);
        }
    }
}