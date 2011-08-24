using System.ComponentModel.DataAnnotations;

namespace Tasks.App.Models
{
    public class NoteCreateModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}