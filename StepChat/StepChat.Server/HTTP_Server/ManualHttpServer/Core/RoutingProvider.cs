using ManualHttpServer.Core.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ManualHttpServer.Core
{
    public class RoutingProvider
    {
        private Dictionary<string, Type> _routes;

        public RoutingProvider()
        {
            _routes = DiscoverRoutes();

            foreach (var route in _routes)
                PrintRoute(route.Key, route.Value.Name);
        }

        private void PrintRoute(string path, string name)
        {
            Console.WriteLine($"Discovered route: {name} | {path}", 
                ConsoleColor.Yellow);
        }

        public IResourceProvider GetResourceProviderByRoute(string route)
        {

            if (_routes.ContainsKey(route))
            {
                var provider = Activator.CreateInstance(_routes[route]);
                return provider as IResourceProvider;
            }
            else
            {
                 throw new InvalidOperationException("Route not found!");
            }
        }

        private string GetAbsolutePathToResource(ResourceRouteAttribute route)
        {
            var path = $"/{route.Route}";
            return path;
        }

        private Dictionary<string, Type> DiscoverRoutes()
        {
            var resources = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(p => typeof(IResourceProvider).IsAssignableFrom(p) &&
                    !p.IsAbstract && !p.IsInterface &&
                    p.GetCustomAttribute(typeof(ResourceRouteAttribute)) != null);

            var routeToResourceMap = resources
                .ToDictionary(p => 
                    GetAbsolutePathToResource(p.GetCustomAttribute<ResourceRouteAttribute>()));

            return routeToResourceMap;
        }
    }
}
