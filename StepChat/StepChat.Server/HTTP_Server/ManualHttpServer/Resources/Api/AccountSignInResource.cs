using ManualHttpServer.Core;
using ManualHttpServer.Core.Attributes;
using ManualHttpServer.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ManualHttpServer.Resources.Api
{
    public class ApplicationAccount
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }


    [ResourceRoute("api/auth/sign-in")]
    public class AccountSignInResource : BaseResourceProvider
    {
        public AccountSignInResource()
        {
        }
        public override void Process(
            HttpListenerRequest request, 
            HttpListenerResponse response)
        {
            var ms = new MemoryStream();
            request.InputStream.CopyTo(ms);

            var bodyAsBytes = ms.ToArray();

            var bodyParsed = JsonConvert.DeserializeObject<ApplicationAccount>(
                Encoding.UTF8.GetString(bodyAsBytes));

            var matchingAccount = Repository.ApplicationAccounts.SingleOrDefault(
                p => 
                    p.Email == bodyParsed.Email && 
                    p.Password.Equals(bodyParsed.Password, StringComparison.InvariantCulture));

            if (matchingAccount == null)
                throw new InvalidOperationException("Bad request");

            var cookie = Guid.NewGuid().ToString("N");
            Repository.ActiveSessions[cookie] = matchingAccount;

            var responseMessage = new
            {
                Cookie = cookie
            };


            var responseBytes = Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(responseMessage));

            response.StatusCode = 200;
            response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
            response.Close();
        }
    }
}
