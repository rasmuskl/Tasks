using System;
using Tasks.Events;

namespace Tasks.Write
{
    public class Fragment : AggregateRoot
    {
        public void CreateFragment(Guid fragmentId, string text, Guid userId)
        {
            ApplyUncommitted(new FragmentCreated(fragmentId, text, userId, DateTime.UtcNow));
        }

        private void Apply(FragmentCreated evt)
        {
            
        }
    }
}