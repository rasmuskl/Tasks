﻿using System.ComponentModel.DataAnnotations;

namespace Tasks.App.Models
{
    public class TaskCreateModel
    {
        [Required]
        public string Title { get; set; }
    }
}