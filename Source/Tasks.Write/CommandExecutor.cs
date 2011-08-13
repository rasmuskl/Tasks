using StructureMap;
using Tasks.Write.CommandHandlers;

namespace Tasks.Write
{
    public class CommandExecutor
    {
        private readonly IContainer _container;

        public CommandExecutor(IContainer container)
        {
            _container = container;
        }

        public void Execute<T>(T command)
        {
            var handler = _container.GetInstance<IHandle<T>>();
            handler.Handle(command);
        }
    }
}