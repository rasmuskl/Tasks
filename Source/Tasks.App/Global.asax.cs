using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;
using System.Linq;
using Tasks.Read.Config;
using Tasks.Write;
using Tasks.Write.Config;

namespace Tasks.App
{
    public class MvcApplication : HttpApplication
    {
        public static DateTime AppStarted { get; set; }

        public void Application_Start()
        {
            new Preloader().Preload(new string[] {});
        }

        public class StructureMapDependencyResolver : IDependencyResolver
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

    public class Preloader : IProcessHostPreloadClient
    {
        public static bool _preloaded;

        public void Preload(string[] parameters)
        {
            if (_preloaded)
                return;

            _preloaded = true;

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ObjectFactory.Initialize(x =>
            {
                x.Scan(scanner =>
                {
                    scanner.TheCallingAssembly();
                    scanner.AssemblyContainingType(typeof(ReadRegistry));
                    scanner.AssemblyContainingType(typeof(WriteRegistry));

                    scanner.LookForRegistries();

                    scanner.WithDefaultConventions();
                });
            });

            DependencyResolver.SetResolver(new MvcApplication.StructureMapDependencyResolver(ObjectFactory.Container));

            Storage.Init();

            MvcApplication.AppStarted = DateTime.Now;
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
    }
}