using System;

namespace Tasks.Read.Queries
{
    public class QueryUserIdByEmail : IQuery<Guid>
    {
        public string Email { get; set; }

        public QueryUserIdByEmail(string email)
        {
            Email = email;
        }
    }
}