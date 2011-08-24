using System;
using System.Collections.Generic;

namespace Tasks.App.Models
{
    public class TasksIndexModel
    {
        public List<string> Tasks { get; set; }
        public List<Tuple<string, string>> Notes { get; set; }
    }
}