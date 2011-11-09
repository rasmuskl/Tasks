using Tasks.Write.Commands;

namespace Tasks.Write.CommandHandlers
{
    public class PrioritizeTaskHandler : ICommandHandler<PrioritizeTask>
    {
        readonly IRepository _repository;

        public PrioritizeTaskHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(PrioritizeTask command)
        {
            var task = _repository.Get<Task>(command.MovedTaskId);
            task.PrioritizeTask(command.UserId, command.RelativeTaskId, command.TaskRelativePriority, command.UtcPrioritized);
            _repository.Commit(command.MovedTaskId, task);
        }
    }
}