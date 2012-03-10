using System;
using System.Linq;
using System.Collections.Generic;
using Tasks.Write.CommandHandlers;

namespace Tasks.Write.Commands
{
    public class NestTask
    {
        public Guid UserId { get; private set; }
        public Guid TaskId { get; private set; }
        public Guid ParentTaskId { get; private set; }
        public DateTime UtcNested { get; private set; }

        public NestTask(Guid userId, Guid taskId, Guid parentTaskId)
        {
            UserId = userId;
            TaskId = taskId;
            ParentTaskId = parentTaskId;
            UtcNested = DateTime.UtcNow;
        }

        public class Handler : ICommandHandler<NestTask>
        {
            private readonly IRepository _repository;

            public Handler(IRepository repository)
            {
                _repository = repository;
            }

            public void Handle(NestTask command)
            {
                var task = _repository.Get<Task>(command.TaskId);
                task.NestTask(command.TaskId, command.ParentTaskId, command.UtcNested);
                _repository.Commit(command.TaskId, task);
            }
        }
    }
}