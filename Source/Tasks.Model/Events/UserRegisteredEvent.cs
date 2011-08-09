using System;

namespace Tasks.Model.Events
{
    public class UserRegisteredEvent
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string PasswordSha1 { get; set; }

        public UserRegisteredEvent(Guid userId, string email, string passwordSha1)
        {
            UserId = userId;
            Email = email;
            PasswordSha1 = passwordSha1;
        }
    }
}