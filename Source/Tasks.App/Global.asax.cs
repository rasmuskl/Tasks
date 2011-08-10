using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;
using System.Linq;
using Tasks.Model;
using Tasks.Model.CommandHandlers;
using Tasks.Model.Events;

namespace Tasks.App
{
    public class MvcApplication : HttpApplication
    {
        public void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ObjectFactory.Initialize(x =>
            {
                x.Scan(scanner =>
                {
                    scanner.TheCallingAssembly();
                    scanner.AssemblyContainingType<Task>();
                    
                    scanner.ConnectImplementationsToTypesClosing(typeof (IHandle<>));
                    scanner.ConnectImplementationsToTypesClosing(typeof (IEventHandler<>));

                    scanner.WithDefaultConventions();
                });
            });

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(ObjectFactory.Container));
        }

        private static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        private class StructureMapDependencyResolver : IDependencyResolver
        {
            private readonly IContainer _container;

            public StructureMapDependencyResolver(IContainer container)
            {
                _container = container;
            }

            public object GetService(Type serviceType)
            {
                if (serviceType.IsAbstract || serviceType.IsInterface)
                {
                    return _container.TryGetInstance(serviceType);
                }

                return _container.GetInstance(serviceType);
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                return ObjectFactory.GetAllInstances(serviceType).OfType<object>();
            }
        }
    }
}