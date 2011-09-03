using System;
using Tasks.Read.Queries;

namespace Tasks.Read.QueryHandlers
{
    public class UserIdByEmailHandler : IQueryHandler<QueryUserIdByEmail, Guid>
    {
        public Guid Handle(QueryUserIdByEmail query)
        {
            return ReadStorage.RegisteredEmails[query.Email];
        }
    }
}