using System;

namespace Tasks.Write
{
    public interface IRepository
    {
        T Get<T>(Guid id) where T : AggregateRoot, new();
        void Commit(Guid id, AggregateRoot aggregate);
    }
}