using ManualHttpServer.Core.Attributes;

using ManualHttpServer.Resources.Api;
using ManualHttpServer.Storage;
using System;
using System.Net;
using System.Reflection;

namespace ManualHttpServer.Core
{
    public class HttpServerProvider
    {
        private readonly HttpListener _httpListener;
        private readonly RoutingProvider _routingProvider;
        private readonly Uri _address;

        public HttpServerProvider(
            Uri address, 
            RoutingProvider routingProvider)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            _address = address;
            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add(_address.ToString());

            _routingProvider = routingProvider;
        }

        public void Start()
        {
            Console.WriteLine(
                $"Trying to start HTTP-server at {_address}", ConsoleColor.Yellow);

            _httpListener.Start();

            Console.WriteLine(
                $"HTTP-server successfully started at {_address}", ConsoleColor.Green);

            while (true)
            {
                try
                {
                    var context = _httpListener.GetContext();
                    OnRequestHandler(context);

                }
                catch (Exception)
                {

                
                }
            
            }     
        }

        public void Stop()
        {
            _httpListener.Stop();

            Console.WriteLine(
                $"HTTP-server successfully stopped at {_address}", ConsoleColor.Red);
        }

        private ApplicationAccount GetCurrentUser(HttpListenerRequest request)
        {
            var cookieHeader = request.Headers["Authorization"];

            if (cookieHeader == null) return null;

            if (Repository.ActiveSessions.ContainsKey(cookieHeader))
            {
                return Repository.ActiveSessions[cookieHeader];
            }

            return null;
        }

        //private bool CanAccessResource(BaseResourceProvider resourceProvider)
        //{
        //    var authorizationAttribute = resourceProvider.GetType()
        //        .GetCustomAttribute<AuthorizationRequired>();

        //    if(authorizationAttribute != null)
        //    {
        //        if (!resourceProvider.IsAuthorized)
        //            return false;
        //    }

        //    return true;
        //}


        public void OnRequestHandler(HttpListenerContext context)
        {
            var rawUrl = context.Request.RawUrl;
            
            // Routing + Resource Handler
            var resourceProvider = _routingProvider.GetResourceProviderByRoute(rawUrl);

            var currentUser = GetCurrentUser(context.Request);

            if(resourceProvider != null)
            {
                var currentAccount = resourceProvider
                    .GetType()
                    .GetProperty("CurrentAccount", BindingFlags.Public | BindingFlags.Instance);

                currentAccount.SetValue(resourceProvider, currentUser);

                // Authorization Access Check
                //if (!CanAccessResource(resourceProvider as BaseResourceProvider))
                //{
                //    context.Response.StatusCode = 403;
                //    context.Response.Close();
                //    return;
                //}

                // Handler Execution
                resourceProvider.Process(context.Request, context.Response);


                // Map request provider result to correct status codes, headers
                // Map exception handling

                // InvalidOperationException -> 400
                // ArgumentNullException -> 400
                // ResourceNotFoundException -> 404

            }
            else
            {
                context.Response.StatusCode = 404;
                context.Response.Close();
            }
        }
    }
}
