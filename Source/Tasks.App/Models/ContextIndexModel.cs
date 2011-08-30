using System.Collections.Generic;
using Tasks.Read.Models;

namespace Tasks.App.Models
{
    public class ContextIndexModel
    {
        public IEnumerable<TaskReadModel> Tasks { get; set; }
        public IEnumerable<NoteReadModel> Notes { get; set; }
        public IEnumerable<ContextReadModel> OtherContexts { get; set; }

    }
}