using System;
using System.Collections.Generic;
using Tasks.Model.Events;

namespace Tasks.Model
{
    public class User
    {
        public User()
        {
            UncommittedEvents = new List<object>();
        }

        public void RegisterUser(Guid userId, string email, string passwordSha1)
        {
            var @event = new UserRegisteredEvent(userId, email, passwordSha1);

            Apply(@event);

            UncommittedEvents.Add(@event);
        }

        private void Apply(UserRegisteredEvent @event)
        {

        }

        public List<object> UncommittedEvents { get; private set; }
    }
}