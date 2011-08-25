using Tasks.Events;

namespace Tasks.Read.EventHandlers
{
    public class UserRegisteredHandler : IEventHandler<UserRegistered>
    {
        public void Handle(UserRegistered evt)
        {
            ReadStorage.RegisteredEmails.Add(evt.Email, evt.UserId);
            ReadStorage.PasswordHashes.Add(evt.Email, evt.PasswordSha1);
        }
    }
}