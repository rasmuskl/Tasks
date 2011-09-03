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
            return Wireup
                .Init()
                .UsingInMemoryPersistence()
                .UsingSynchronousDispatcher(new DelegateMessagePublisher(x => _eventsPublished.AddRange(x.Events.Select(e => e.Body).ToList())))
                .Build();
        }
    }
}