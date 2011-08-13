namespace Tasks.Write.CommandHandlers
{
    public interface IHandle<T>
    {
        void Handle(T command);
    }
}