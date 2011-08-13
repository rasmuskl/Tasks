using System;

namespace Tasks.Write.Commands
{
    public class CreateTask
    {
        public CreateTask(string task)
        {
            Task = task;
            TaskId = Guid.NewGuid();
        }

        public Guid TaskId { get; private set; }
        public string Task { get; private set; }
    }
}