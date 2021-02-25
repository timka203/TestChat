using ManualHttpServer.Core;
using ManualHttpServer.Resources.Api;
using ManualHttpServer.Storage;
using System;

namespace StepChat.Server
{
    public class ChatHTTP_Server
    {
        private static RoutingProvider _routingProvider;
        private static HttpServerProvider _httpServerProvider;
        public static void StartServer()
        {
          

            var address = new Uri("http://127.0.0.1:15006/");

            _routingProvider = new RoutingProvider();
            _httpServerProvider = new HttpServerProvider(address, _routingProvider);
            
            _httpServerProvider.Start();

            Console.ReadLine();
        }
    }
}
