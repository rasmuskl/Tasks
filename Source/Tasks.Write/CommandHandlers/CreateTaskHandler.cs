using Tasks.Write.Commands;

namespace Tasks.Write.CommandHandlers
{
    public class CreateTaskHandler : ICommandHandler<CreateTask>
    {
        readonly IRepository _repository;

        public CreateTaskHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CreateTask command)
        {
            var task = _repository.Get<Task>(command.TaskId);
            task.CreateTask(command.Title, command.TaskId, command.UserId, command.UtcCreated);
            _repository.Commit(command.TaskId, task);
        }
    }
}