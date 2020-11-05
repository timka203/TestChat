using ManualHttpServer.Resources.Api;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ManualHttpServer.Core
{
    public abstract class BaseResourceProvider : IResourceProvider
    {
        public bool IsAuthorized  => CurrentAccount != null;
        public ApplicationAccount CurrentAccount { get; set; }
        public BaseResourceProvider()
        {

        }

        public abstract void Process(
            HttpListenerRequest request,
            HttpListenerResponse response);
    }
}
