using System;

namespace Tasks.Write.Commands
{
    public class CreateContext
    {
        public Guid ContextId { get; set; }
        public string ContextName { get; set; }
        public DateTime UtcCompleted { get; set; }
        public Guid UserId { get; set; }

        public CreateContext(Guid contextId, string contextName, Guid userId)
        {
            ContextId = contextId;
            ContextName = contextName;
            UserId = userId;
            UtcCompleted = DateTime.UtcNow;
        }
    }
}