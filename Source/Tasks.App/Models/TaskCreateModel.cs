using System;
using System.ComponentModel.DataAnnotations;

namespace Tasks.App.Models
{
    public class TaskCreateModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public Guid ContextId { get; set; }

        public Guid PrevTaskId { get; set; }
    }
}