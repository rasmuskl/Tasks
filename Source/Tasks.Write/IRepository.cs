using System;

namespace Tasks.Write
{
    public interface IRepository
    {
        T Get<T>(Guid id) where T : AggregateRoot, new();
        AggregateRoot Get(Type type, Guid id);
        void Commit(Guid id, AggregateRoot aggregate);
    }
}