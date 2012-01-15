using System;

namespace Tasks.Write.Commands
{
    public class CreateFragment
    {
        public string Text { get; private set; }
        public Guid UserId { get; private set; }
        public Guid FragmentId { get; private set; }

        public CreateFragment(string text, Guid userId)
        {
            Text = text;
            UserId = userId;
            FragmentId = Guid.NewGuid();
        }
    }
}