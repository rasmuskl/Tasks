using System;
using System.Collections.Generic;
using Tasks.Events;

namespace Tasks.Write
{
    public class User : AggregateRoot
    {
        public void RegisterUser(Guid userId, string email, string passwordSha1)
        {
            ApplyUncommitted(new UserRegistered(userId, email, passwordSha1));
        }

        private void Apply(UserRegistered evt)
        {

        }
    }
}