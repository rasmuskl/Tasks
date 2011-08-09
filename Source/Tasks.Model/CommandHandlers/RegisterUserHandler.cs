using System;
using EventStore;
using Tasks.Model.Commands;

namespace Tasks.Model.CommandHandlers
{
    public class RegisterUserHandler : IHandle<RegisterUserCommand>
    {
        public void Handle(RegisterUserCommand command)
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