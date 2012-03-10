using System;
using System.Linq;
using System.Collections.Generic;
using Tasks.Events;
using Tasks.Read.Models;

namespace Tasks.Read.EventHandlers
{
    public class TaskNestedHandler : IEventHandler<TaskNested>
    {
        public void Handle(TaskNested evt)
        {
        }
    }
}