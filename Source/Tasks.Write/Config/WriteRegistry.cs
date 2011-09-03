using StructureMap.Configuration.DSL;
using Tasks.Write.CommandHandlers;

namespace Tasks.Write.Config
{
    public class WriteRegistry : Registry
    {
        public WriteRegistry()
        {
            Scan(s =>
                {
                    s.TheCallingAssembly();
                    s.WithDefaultConventions();

                    s.ConnectImplementationsToTypesClosing(typeof(ICommandHandler<>));
                });
        }
    }
}