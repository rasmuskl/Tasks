using System;
using Tasks.Events;

namespace Tasks.Write
{
    public class Context : AggregateRoot
    {
        public void CreateContext(Guid contextId, string contextName, Guid userId, DateTime utcCompleted)
        {
            ApplyUncommitted(new ContextCreated(contextId, contextName, userId, utcCompleted));
        }

        private void Apply(ContextCreated evt)
        {
            
        }
    }
}