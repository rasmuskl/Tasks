using System;

namespace Tasks.Model.Commands
{
    public class CreateTaskCommand
    {
        public CreateTaskCommand(string task)
        {
            Task = task;
            TaskId = Guid.NewGuid();
        }

        public Guid TaskId { get; private set; }
        public string Task { get; private set; }
    }
}