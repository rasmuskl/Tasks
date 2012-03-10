using System;
using System.Linq;
using System.Collections.Generic;
using EventStore;
using NUnit.Framework;
using StructureMap;
using Tasks.Read;
using Tasks.Read.Config;

namespace Tasks.Tests.Read
{
    public abstract class NUReadContext
    {
        [TestFixtureSetUp]
        public void BeforeAllTests()
        {
            InitializeContainer();
            Given();
            When();
        }

        protected abstract void Given();
        protected abstract void When();

        private static void InitializeContainer()
        {
            ObjectFactory.Initialize(i =>
            {
                i.Scan(s =>
                {
                    s.AssemblyContainingType<ReadRegistry>();
                    s.LookForRegistries();
                });
            });
        }

        protected static T ProcessedEvent<T>(T evt)
        {
            var eventMessages = new List<EventMessage>
                                    {
                                        new EventMessage { Body = evt },
                                    };

            var commit = new Commit(Guid.NewGuid(), 0, Guid.NewGuid(), 0, DateTime.Now, null, eventMessages);
            ReadStorage.HandleCommit(commit);

            return evt;
        }
    }
}