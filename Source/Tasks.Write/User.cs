using System;
using System.Collections.Generic;
using Tasks.Events;

namespace Tasks.Write
{
    public class User : AggregateRoot
    {
        public void RegisterUser(Guid userId, string email, string passwordSha1)
        {
            var @event = new UserRegistered(userId, email, passwordSha1);

            Apply(@event);

            UncommittedEvents.Add(@event);
        }

        private void Apply(UserRegistered @event)
        {

        }
    }
}