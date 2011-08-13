using System;

namespace Tasks.Events
{
    public class UserRegistered
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string PasswordSha1 { get; set; }

        public UserRegistered(Guid userId, string email, string passwordSha1)
        {
            UserId = userId;
            Email = email;
            PasswordSha1 = passwordSha1;
        }
    }
}