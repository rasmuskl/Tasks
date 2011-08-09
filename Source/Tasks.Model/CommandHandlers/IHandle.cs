namespace Tasks.Model.CommandHandlers
{
    public interface IHandle<T>
    {
        void Handle(T command);
    }
}