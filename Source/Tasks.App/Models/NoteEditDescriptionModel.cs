using System.ComponentModel.DataAnnotations;

namespace Tasks.App.Models
{
    public class NoteEditDescriptionModel
    {
        [ScaffoldColumn(false)]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}