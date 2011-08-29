using System;

namespace Tasks.Events
{
    public class ContextCreated
    {
        public ContextCreated(Guid contextId, string contextName, Guid userId, DateTime utcCompleted)
        {
            ContextId = contextId;
            ContextName = contextName;
            UserId = userId;
            UtcCompleted = utcCompleted;
        }

        public Guid UserId { get; set; }
        public DateTime UtcCompleted { get; set; }
        public string ContextName { get; set; }
        public Guid ContextId { get; set; }
    }
}