using ManualHttpServer.Core;
using ManualHttpServer.Core.Attributes;
using ManualHttpServer.Storage;
using Newtonsoft.Json;
using StepChat.Common.CommonModels;
using StepChat.Server;
using StepChat.Server.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using WebSocketSharp;

namespace ManualHttpServer.Resources.Api
{
    [ResourceRoute("api/auth/login")]
    public class AccountLogInResource : BaseResourceProvider
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

            var bodyParsed = JsonConvert.DeserializeObject<User>(
                Encoding.UTF8.GetString(bodyAsBytes));
            if (DB_Functions.Find_User(bodyParsed.ContactNickname, bodyParsed.Password))
            {
                //response.OutputStream.Write(DB_Functions.Find_User(bodyParsed.ContactNickname, bodyParsed.Password).ToByteArray(ByteOrder.Big), 0, DB_Functions.Find_User(bodyParsed.ContactNickname, bodyParsed.Password).ToByteArray(ByteOrder.Big).Length);

              
                
                Repository.ApplicationAccounts.Add(new ApplicationAccount()
                {
                    Email = bodyParsed.ContactNickname,
                    Password = bodyParsed.Password
                });
                var cookie = Guid.NewGuid().ToString("N");
                Repository.ActiveSessions[cookie] = new ApplicationAccount()
                {
                    Email = bodyParsed.ContactNickname,
                    Password = bodyParsed.Password
                };

            
                webSocket.Connect();
                webSocket.Send(JsonConvert.SerializeObject(new MessageContainer<KeyValuePair<string, string>>() { MessageType = MessageTypes.FromHttpToWeb, Body = new KeyValuePair<string, string>(bodyParsed.ContactNickname, cookie) }));


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
}
