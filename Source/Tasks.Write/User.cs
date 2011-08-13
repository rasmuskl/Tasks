using System;
using System.Collections.Generic;
using Tasks.Events;

namespace Tasks.Write
{
    public class User
    {
        public User()
        {
            UncommittedEvents = new List<object>();
        }

        public void RegisterUser(Guid userId, string email, string passwordSha1)
        {
            var @event = new UserRegistered(userId, email, passwordSha1);

            Apply(@event);

            UncommittedEvents.Add(@event);
        }

        private void Apply(UserRegistered @event)
        {

        }

        public List<object> UncommittedEvents { get; private set; }
    }
}