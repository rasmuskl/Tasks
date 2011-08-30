using System.ComponentModel.DataAnnotations;

namespace Tasks.App.Models
{
    public class ContextCreateModel
    {
        [Required]
        public string Name { get; set; }
    }
}