using Newtonsoft.Json;
using StepChat.Common.CommonModels;
using StepChat.Common.ViewModels;
using StepChat.Server.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace StepChat.Server
{
   
        class SocketConnectionHandler : WebSocketBehavior
        {
            WebSocketServer WebSocketServer;
            public SocketConnectionHandler(WebSocketServer ws)
            {
                WebSocketServer = ws;

            GroupList = DB_Functions.GetGroups();

            }

            public static Dictionary<string, string> idList = new Dictionary<string, string>();
        public static Dictionary<string, string> HTTPidList = new Dictionary<string, string>();
        public static List<Local_Group> GroupList = new List<Local_Group>();



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
                        {


                        }
                        break;
                    case MessageTypes.FromHttpToWeb:
                       {

                        MessageContainer<KeyValuePair<string, string>> message = JsonConvert.DeserializeObject<MessageContainer<KeyValuePair<string, string>>>(e.Data);

                        HTTPidList.Add(message.Body.Key, message.Body.Value);

                    }
                break;
                    case MessageTypes.NewIncomingMessage:
                        {

                            MessageContainer<MessageTo> message = JsonConvert.DeserializeObject<MessageContainer<MessageTo>>(e.Data);
                            
                                DB_Functions.Add_Message(message.Body);

                        foreach (var item in GroupList.Where(w=>w.GroupId==message.Body.To).SingleOrDefault().Users)
                        {


                            Sender.SendMessageToClient(WebSocketServer, idList.Where(w => w.Key == item.ContactNickname).FirstOrDefault().Value, e.Data);
                        }

                        }
                        break;
                    case MessageTypes.AuthMessage:
                        {

                        MessageContainer<KeyValuePair<string, string>> message = JsonConvert.DeserializeObject<MessageContainer<KeyValuePair<string, string>>>(e.Data);

                        if (HTTPidList.Any(w=>w.Key==message.Body.Key&& w.Value == message.Body.Value))
                        {
                            idList.Add(message.Body.Key, ID);
                        }

                            
                        }
                        break;
                    case MessageTypes.ToServerMessage:
                        {
                            MessageContainer<KeyValuePair<string, string>> message = JsonConvert.DeserializeObject<MessageContainer<KeyValuePair<string, string>>>(e.Data);
                          
                                List<MessageTo> messages = new List<MessageTo>();
                                try
                                {
                                    messages =  DB_Functions.GetMessages(message.Body.Key);


                            foreach (var item in GroupList.Where(w => w.GroupId == message.Body.Key).SingleOrDefault().Users)
                            {


                                Sender.SendMessageToClient(WebSocketServer, idList.Where(w => w.Key == item.ContactNickname).FirstOrDefault().Value, JsonConvert.SerializeObject(new MessageContainer<MessagesInfo>() { MessageType = MessageTypes.ToServerMessage, Body = new MessagesInfo() { messages = messages} }));

                            }

                        }
                                catch (Exception)
                                {

                                }
                            
                        }
                        break;

                    default:
                        break;
                }

            }

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


       public class WebServer
        {
            
            static void BroadcastContactStatuses(WebSocketServer ws,
                List<ContactItemModel> contacts)
            {
                var container = new MessageContainer<List<ContactItemModel>>()
                {
                    MessageType = MessageTypes.ContactListStatusUpdate,
                    Body = contacts
                };


                var message = JsonConvert.SerializeObject(container);

                ws.WebSocketServices
                    .TryGetServiceHost("/", out WebSocketServiceHost host);

                foreach (var session in host.Sessions.Sessions.ToList())
                {
                    Sender.SendMessageToClient(ws, session.ID, message);
                }
            foreach (var item in SocketConnectionHandler.GroupList)
            {
                foreach (var user in item.Users)
                {try
                    {
                        if (SocketConnectionHandler.idList.Count()!=0)
                        {
                            if (SocketConnectionHandler.idList.Any(w => w.Key == user.ContactNickname))
                            {
                                Sender.SendMessageToClient(ws, SocketConnectionHandler.idList.Where(w => w.Key == user.ContactNickname).SingleOrDefault().Value, JsonConvert.SerializeObject(new MessageContainer<List<Local_Group>>() { MessageType = MessageTypes.GroupListUpdate, Body = SocketConnectionHandler.GroupList.Where(w => w.Users.Any(a => a.ContactNickname == user.ContactNickname)).ToList() }));

                            }

                        }
                        }
                    catch { 
                    }

                }

            }
        }



            static Task BroadcastTimeJob(WebSocketServer ws)
            {
               
            var contacts = DB_Functions.GetUsers();

                var job = Task.Run(() =>
                {
                    while (true)
                    {
                        List<ContactItemModel> users = new List<ContactItemModel>();
                        foreach (var item in contacts)
                        {
                            users.Add(new ContactItemModel() { ContactNickname = item.ContactNickname, IsOnline = SocketConnectionHandler.idList.Any(w => w.Key == item.ContactNickname), ApplicationUserId=item.Id });
                        }
                      SocketConnectionHandler.GroupList = DB_Functions.GetGroups();

                        BroadcastContactStatuses(ws, users);

                        Thread.Sleep(TimeSpan.FromSeconds(2));
                  }
                });

                return job;
            }

           public static void StartServer()
            {
                var address = "ws://192.168.1.7:15000";
                var ws = new WebSocketServer(address);
       
            



                ws.Start();

                ws.AddWebSocketService("/", () =>
                    new SocketConnectionHandler(ws));

                

                BroadcastTimeJob(ws);

                Console.ReadLine();
            }
        }
    }

