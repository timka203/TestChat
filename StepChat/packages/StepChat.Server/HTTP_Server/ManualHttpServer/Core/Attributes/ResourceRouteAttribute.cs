using System;

namespace ManualHttpServer.Core.Attributes
{
    public class ResourceRouteAttribute : Attribute
    {
        public string Route { get; private set; }
        public ResourceRouteAttribute(string route)
        {
            Route = route;
        }
    }
}
