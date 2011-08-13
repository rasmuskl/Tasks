using System;
using EventStore;
using Tasks.Write.Commands;

namespace Tasks.Write.CommandHandlers
{
    public class RegisterUserHandler : IHandle<RegisterUser>
    {
        public void Handle(RegisterUser command)
        {
            using(IEventStream stream = Storage.Store.CreateStream(command.UserId))
            {
                var user = new User();
                user.RegisterUser(command.UserId, command.Email, command.PasswordSha1);

                foreach (var @event in user.UncommittedEvents)
                {
                    stream.Add(new EventMessage { Body = @event });
                }

                stream.CommitChanges(Guid.NewGuid());
            }
        }
    }
}