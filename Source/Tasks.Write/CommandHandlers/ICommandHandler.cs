namespace Tasks.Write.CommandHandlers
{
    public interface ICommandHandler<T>
    {
        void Handle(T command);
    }
}