using System;
using System.Linq;
using System.Collections.Generic;
using EventStore;
using EventStore.Dispatcher;
using NUnit.Framework;
using StructureMap;
using Tasks.Write;
using Tasks.Write.Config;

namespace Tasks.Tests.Write
{
    public abstract class NUWriteContext
    {
        protected static List<object> _eventsPublished;
        protected static CommandExecutor _executor;

        [TestFixtureSetUp]
        public void BeforeEachTest()
        {
            _eventsPublished = new List<object>();
            InitializeWriteContext();
            Given();
            When();
        }

        protected abstract void Given();
        protected abstract void When();

        private void InitializeWriteContext()
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

        protected void AddToHistory(Guid entityId, object evt)
        {
            var storeEvents = ObjectFactory.GetInstance<IStoreEvents>();

            using (var stream = storeEvents.OpenStream(entityId, 0, Int32.MaxValue))
            {
                stream.Add(new EventMessage { Body = evt });
                stream.CommitChanges(Guid.NewGuid());
            }

            _eventsPublished.Clear();
        }

        private IStoreEvents InitializeEventStore()
        {
            return Wireup.Init()
                .UsingInMemoryPersistence()
                .UsingSynchronousDispatcher(new DelegateMessagePublisher(x => _eventsPublished.AddRange(x.Events.Select(e => e.Body).ToList())))
                .Build();
        }
         
    }
}