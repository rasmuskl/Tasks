using System;
using System.Collections.Generic;
using EventStore;
using Machine.Specifications;
using StructureMap;
using Tasks.Read;
using Tasks.Read.Config;

namespace Tasks.Tests.Read
{
    public class ReadContext
    {
        Establish readContext = () =>
            {
                InitializeContext();
            };

        private static void InitializeContext()
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