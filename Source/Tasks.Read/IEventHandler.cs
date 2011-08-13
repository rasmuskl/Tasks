namespace Tasks.Read
{
    public interface IEventHandler<T>
    {
        void Handle(T @event);
    }
}