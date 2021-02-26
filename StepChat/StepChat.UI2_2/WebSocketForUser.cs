using Newtonsoft.Json;
using StepChat.Common.CommonModels;
using StepChat.Server.DB;

using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace StepChat.UI2_2
{
   public static  class WebSocketForUser
    {
      
        private static WebSocketServer server;

       

        public static WebSocket webSocket;
        private static void OnMessageHandler(object sender, MessageEventArgs e)
        {
            switch (JsonConvert.DeserializeObject<MessageContainer<object>>(e.Data).MessageType)
            {
                case MessageTypes.ContactListStatusUpdate:
                    {
                        Sender.SendMessageToClient(server, AppConnectionHandler.AppList.Where(w => w.Value == "ChatWindow").FirstOrDefault().Key, e.Data);


                    }
                    break;
                case MessageTypes.GroupListUpdate:
                    {
                        Sender.SendMessageToClient(server, AppConnectionHandler.AppList.Where(w => w.Value == "ChatWindow").FirstOrDefault().Key, e.Data);


                    }
                    break;
                case MessageTypes.NewIncomingMessage:
                    {
                        foreach (var item in AppConnectionHandler.AppList.Where(w => w.Value == JsonConvert.DeserializeObject<MessageContainer<MessageTo>>(e.Data).Body.To))
                        {


                            Sender.SendMessageToClient(server, item.Key, e.Data);
                        }


                    }
                    break;
                case MessageTypes.AuthMessage:
                    {
                        



                    }
                    break;
                case MessageTypes.ToServerMessage:
                    {
                        SendToServer(e);
                        

                    }
                    break;

                default:
                    break;
            }

    }
        private static void SendToServer(MessageEventArgs e)
        {
            foreach (var item in AppConnectionHandler.AppList.Where(w => w.Value == JsonConvert.DeserializeObject<MessageContainer<MessagesInfo>>(e.Data).Body.messages[0].To))
            {


                Sender.SendMessageToClient(server, item.Key, e.Data);
            }
        }

        public static void Start(string url)
        {
            webSocket = new WebSocket("ws://192.168.1.7:15000");
            server = new WebSocketServer(url);
            
            webSocket.OnMessage += OnMessageHandler;
            webSocket.Connect();
            server.AddWebSocketService("/", () =>
               new AppConnectionHandler(server));
            server.Start();

           

        }

        public static void AuthUser(string nickname,string cookie )
        { 
            webSocket.Send(JsonConvert.SerializeObject(new MessageContainer<KeyValuePair<string, string>>() { MessageType = MessageTypes.AuthMessage, Body = new KeyValuePair<string, string>(nickname, cookie) }));
          
        }
        class AppConnectionHandler : WebSocketBehavior
        {
            WebSocketServer WebSocketServer;
           
            public AppConnectionHandler(WebSocketServer ws)
            {
                WebSocketServer = ws;
            }

            public static Dictionary<string, string> AppList = new Dictionary<string, string>();



            protected override void OnOpen()
            {
                Console.WriteLine($"Opened connection to {ID}");
                base.OnOpen();
            }
            protected override void OnMessage(MessageEventArgs e)
            {

                switch (JsonConvert.DeserializeObject<MessageContainer<object>>(e.Data).MessageType)
                {
                    case MessageTypes.ContactListStatusUpdate:

                        break;
                    case MessageTypes.NewIncomingMessage:
                        {
                            webSocket.Send(e.Data);
                        }
                        break;
                    case MessageTypes.AuthMessage:
                        break;
                    case MessageTypes.AppMessage:
                        {
                            AppList.Add(ID, JsonConvert.DeserializeObject<MessageContainer<string>>(e.Data).Body);

                            
                        }
                        break;
                    case MessageTypes.ToServerMessage:
                        {

                            webSocket.Send(e.Data);
                        }
                        break;
                    default:
                        break;
                }







            }

        }

      
        //static void Main(string[] args)
        //{
        //    var address = "ws://127.0.0.1:15001";
        //    var ws = new WebSocketServer(address);



        //    ws.Start();

        //    ws.AddWebSocketService("/", () =>
        //        new AppConnectionHandler(ws));

            

           

        //    Console.ReadLine();
        //}

    }
    public class Sender
    {
        public static void SendMessageToClient(WebSocketServer ws,
           string clientId, string message)
        {
            ws.WebSocketServices
                .TryGetServiceHost("/", out WebSocketServiceHost host);

            if (host != null)
            {
                host.Sessions.TryGetSession(clientId,
                    out IWebSocketSession webSocketSession);

                if (webSocketSession != null)
                {
                    host.Sessions.SendTo(message, webSocketSession.ID);
                }
            }

        }
    }
}
