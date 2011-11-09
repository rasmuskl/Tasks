using Tasks.Write.Commands;

namespace Tasks.Write.CommandHandlers
{
    public class CompleteTaskHandler : ICommandHandler<CompleteTask>
    {
        readonly IRepository _repository;

        public CompleteTaskHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CompleteTask command)
        {
            var task = _repository.Get<Task>(command.TaskId);
            task.CompleteTask(command.UtcCompleted, command.UserId);
            _repository.Commit(command.TaskId, task);
        }
    }
}