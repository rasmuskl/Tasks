using System;
using System.Collections.Generic;
using Tasks.Read.Models;

namespace Tasks.App.Models
{
    public class TasksIndexModel
    {
        public List<TaskReadModel> Tasks { get; set; }
        public List<Tuple<string, string>> Notes { get; set; }
    }
}