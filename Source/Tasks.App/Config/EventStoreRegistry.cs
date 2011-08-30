using EventStore;
using EventStore.Dispatcher;
using StructureMap.Configuration.DSL;
using Tasks.Read;

namespace Tasks.App.Config
{
    public class EventStoreRegistry : Registry
    {
        public EventStoreRegistry()
        {
            For<IStoreEvents>().Singleton().Add(Wireup
                .Init()
                .UsingSqlPersistence("conn")
                .UsingJsonSerialization()
                .UsingSynchronousDispatcher(
                    new DelegateMessagePublisher(ReadStorage.HandleCommit))
                .Build());
        }
    }
}