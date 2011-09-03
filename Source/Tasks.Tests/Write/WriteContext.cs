using System;
using System.Collections.Generic;
using System.Linq;
using EventStore;
using EventStore.Dispatcher;
using Machine.Specifications;
using StructureMap;
using Tasks.Write;
using Tasks.Write.Config;

namespace Tasks.Tests.Write
{
    public class WriteContext
    {
        protected static List<object> _eventsPublished;
        protected static CommandExecutor _executor;

        Establish writeContext = () =>
            {
                _eventsPublished = new List<object>();
                InitializeWriteContext();
            };

        protected static void AddToHistory(Guid entityId, object evt)
        {
            var storeEvents = ObjectFactory.GetInstance<IStoreEvents>();

            using(var stream = storeEvents.OpenStream(entityId, 0, Int32.MaxValue))
            {
                stream.Add(new EventMessage { Body = evt });
                stream.CommitChanges(Guid.NewGuid());
            }

            _eventsPublished.Clear();
        }

        private static void InitializeWriteContext()
        {
            ObjectFactory.Initialize(i =>
                {
                    i.Scan(s =>
                        {
                            s.AssemblyContainingType<WriteRegistry>();
                            s.LookForRegistries();
                        });

                    i.For<IStoreEvents>()
                        .Singleton()
                        .Use(InitializeEventStore);
                }
                );

            _executor = ObjectFactory.GetInstance<CommandExecutor>();
        }

        private static IStoreEvents InitializeEventStore()
        {
            return Wireup.Init()
                .UsingInMemoryPersistence()
                .UsingSynchronousDispatcher(new DelegateMessagePublisher(x => _eventsPublished.AddRange(x.Events.Select(e => e.Body).ToList())))
                .Build();
        }
    }
}