using System;

namespace Tasks.Write.Commands
{
    public class CreateTask
    {
        public CreateTask(string title)
        {
            Title = title;
            TaskId = Guid.NewGuid();
        }

        public Guid TaskId { get; private set; }
        public string Title { get; private set; }
    }
}