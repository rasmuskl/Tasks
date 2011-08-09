using System;
using System.Collections.Generic;
using EventStore;
using EventStore.Dispatcher;
using Tasks.Model.Events;

namespace Tasks.Model
{
    public static class Storage
    {
        private static readonly IStoreEvents _store = Wireup
                    .Init()
                    .UsingInMemoryPersistence()
                    .UsingJsonSerialization()
                    .UsingSynchronousDispatcher(new DelegateMessagePublisher(HandleCommit))
                    .Build();

        private static void HandleCommit(Commit commit)
        {
            foreach (var eventMessage in commit.Events)
            {
                if(eventMessage.Body is TaskCreatedEvent)
                {
                    var @event = eventMessage.Body as TaskCreatedEvent;
                    Tasks.Add(@event.TaskCaption);
                }

                if(eventMessage.Body is UserRegisteredEvent)
                {
                    var @event = eventMessage.Body as UserRegisteredEvent;
                    RegisteredEmails.Add(@event.Email);
                    PasswordHashes.Add(@event.Email, @event.PasswordSha1);
                }
            }
        }

        public static List<string> Tasks { get; private set; }
        public static HashSet<string> RegisteredEmails { get; private set; }
        public static Dictionary<string, string> PasswordHashes { get; private set; }

        static Storage()
        {
            Tasks = new List<string>();
            RegisteredEmails = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            PasswordHashes = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        }

        public static IStoreEvents Store
        {
            get { return _store; }
        }
    }
}