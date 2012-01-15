using System;

namespace Tasks.Events
{
    public class FragmentCreated
    {
        public Guid FragmentId { get; set; }
        public string Text { get; set; }
        public Guid UserId { get; set; }
        public DateTime UtcCreated { get; set; }

        public FragmentCreated(Guid fragmentId, string text, Guid userId, DateTime utcCreated)
        {
            FragmentId = fragmentId;
            Text = text;
            UserId = userId;
            UtcCreated = utcCreated;
        }
    }
}