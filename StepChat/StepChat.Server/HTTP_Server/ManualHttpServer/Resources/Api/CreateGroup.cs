using ManualHttpServer.Core;
using ManualHttpServer.Core.Attributes;
using ManualHttpServer.Resources.Api;
using ManualHttpServer.Storage;
using Newtonsoft.Json;
using StepChat.Common.CommonModels;
using StepChat.Server.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace ManualHttpServer.Resources.Api
{
    [ResourceRoute("api/auth/CreateGroup")]
    public class CreateGroup : BaseResourceProvider
    {
        WebSocket webSocket = new WebSocket("ws://192.168.1.7:15000");
        private void ProcessInternal()
        {
        }
        public override void Process(
            HttpListenerRequest request,
            HttpListenerResponse response)
        {
            var ms = new MemoryStream();
            request.InputStream.CopyTo(ms);

            var bodyAsBytes = ms.ToArray();

            var bodyParsed = JsonConvert.DeserializeObject<Local_Group>(
                Encoding.UTF8.GetString(bodyAsBytes));

            //response.OutputStream.Write(DB_Functions.Find_User(bodyParsed.ContactNickname, bodyParsed.Password).ToByteArray(ByteOrder.Big), 0, DB_Functions.Find_User(bodyParsed.ContactNickname, bodyParsed.Password).ToByteArray(ByteOrder.Big).Length);







            var responseMessage = new { Id = DB_Functions.Create_Group(bodyParsed.Users, bodyParsed.Title, bodyParsed.GroupType ) };
               


                var responseBytes = Encoding.UTF8.GetBytes(
                    JsonConvert.SerializeObject(responseMessage));
                response.StatusCode = 200;

                response.OutputStream.Write(responseBytes, 0, responseBytes.Length);


                response.Close();


            






        }
    }
}
