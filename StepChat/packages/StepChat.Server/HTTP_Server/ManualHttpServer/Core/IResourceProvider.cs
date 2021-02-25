using System.Net;

namespace ManualHttpServer.Core
{
    public interface IResourceProvider
    {
         void Process(
            HttpListenerRequest request,
            HttpListenerResponse response);
       
    }
}
