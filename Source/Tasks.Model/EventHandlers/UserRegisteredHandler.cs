using Tasks.Model.Events;

namespace Tasks.Model.EventHandlers
{
    public class UserRegisteredHandler : IEventHandler<UserRegisteredEvent>
    {
        public void Handle(UserRegisteredEvent @event)
        {
            Storage.RegisteredEmails.Add(@event.Email);
            Storage.PasswordHashes.Add(@event.Email, @event.PasswordSha1);
        }
    }
}