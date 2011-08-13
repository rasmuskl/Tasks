using EventStore;
using EventStore.Dispatcher;
using Tasks.Read;

namespace Tasks.Write
{
    public static class Storage
    {
        private static IStoreEvents _store = Wireup
                    .Init()
                    .UsingInMemoryPersistence()
                    .UsingBinarySerialization()
                    .UsingSynchronousDispatcher(new DelegateMessagePublisher(ReadStorage.HandleCommit))
                    .Build();


        public static IStoreEvents Store
        {
            get { return _store; }
            set { _store = value; }
        }
    }
}