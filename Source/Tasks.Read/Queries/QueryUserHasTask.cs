using System;

namespace Tasks.Read.Queries
{
    public class QueryUserHasTask : IQuery<bool>
    {
        public Guid UserId { get; set; }
        public Guid TaskId { get; set; }

        public QueryUserHasTask(Guid userId, Guid taskId)
        {
            UserId = userId;
            TaskId = taskId;
        }
    }
}