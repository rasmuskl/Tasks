using Tasks.Events;

namespace Tasks.Read.EventHandlers
{
    public class UserRegisteredHandler : IEventHandler<UserRegistered>
    {
        public void Handle(UserRegistered @event)
        {
            ReadStorage.RegisteredEmails.Add(@event.Email, @event.UserId);
            ReadStorage.PasswordHashes.Add(@event.Email, @event.PasswordSha1);
        }
    }
}