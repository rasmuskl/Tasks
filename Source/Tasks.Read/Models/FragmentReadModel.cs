using System;

namespace Tasks.Read.Models
{
    public class FragmentReadModel
    {
        public Guid FragmentId { get; set; }
        public string Text { get; set; }
        public DateTime UtcCreated { get; set; }

        public FragmentReadModel(Guid fragmentId, string text, DateTime utcCreated)
        {
            FragmentId = fragmentId;
            Text = text;
            UtcCreated = utcCreated;
        }
    }
}