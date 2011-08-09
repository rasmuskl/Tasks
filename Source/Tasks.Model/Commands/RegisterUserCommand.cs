using System;

namespace Tasks.Model.Commands
{
    public class RegisterUserCommand
    {
        public RegisterUserCommand(string email, string passwordSha1)
        {
            Email = email;
            PasswordSha1 = passwordSha1;
            UserId = Guid.NewGuid();
        }

        public Guid UserId { get; private set; }
        public string Email { get; private set; }
        public string PasswordSha1 { get; private set; }
    }
}