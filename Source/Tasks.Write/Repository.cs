using System;
using EventStore;

namespace Tasks.Write
{
    public class Repository : IRepository
    {
        public T Get<T>(Guid id) where T : AggregateRoot, new()
        {
            using(var stream = Storage.Store.OpenStream(id, 0, int.MaxValue))
            {
                var aggregate = new T();

                foreach (var committedEvent in stream.CommittedEvents)
                {
                    aggregate.ApplyCommitted(committedEvent.Body);
                }

                return aggregate;
            }
        }

        public void Commit(Guid id, AggregateRoot aggregate)
        {
            using (var stream = Storage.Store.OpenStream(id, 0, int.MaxValue))
            {

                foreach (var uncommittedEvent in aggregate.UncommittedEvents)
                {
                    stream.Add(new EventMessage {Body = uncommittedEvent});
                }

                stream.CommitChanges(Guid.NewGuid());
            }
        }
    }
}