using StructureMap.Configuration.DSL;

namespace Tasks.Read.Config
{
    public class ReadRegistry : Registry
    {
        public ReadRegistry()
        {
            Scan(s =>
                {
                    s.TheCallingAssembly();
                    s.WithDefaultConventions();

                    s.ConnectImplementationsToTypesClosing(typeof (IEventHandler<>));
                    s.ConnectImplementationsToTypesClosing(typeof (IQueryHandler<,>));

                });
        }
    }
}