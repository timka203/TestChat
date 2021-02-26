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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebSocketSharp;

namespace StepChat.UI2_2
{
    /// <summary>
    /// Логика взаимодействия для ChatPage.xaml
    /// </summary>
    public partial class ChatPage : Page
    {
        private WebSocket _webSocket;

        private string user;

        private string GroupId;
        public ChatPage(string user, string GroupId, string url)
        {
            _webSocket = new WebSocket(url);
            this.user = user;
            this.GroupId = GroupId;
            AcceptSocketMessages();
            InitializeComponent();
        }
        //websocket logic
        private void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var containerMetainfo = JsonConvert
                .DeserializeObject<MessageContainer<object>>(e.Data);

            if (containerMetainfo.MessageType == MessageTypes.NewIncomingMessage)
            {
                var concreteContainer = JsonConvert
                    .DeserializeObject<MessageContainer<MessageTo>>(e.Data);
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    Add_Message(concreteContainer.Body);




                }));



            }
            else if (containerMetainfo.MessageType == MessageTypes.ToServerMessage)
            {
                var concreteContainer = JsonConvert
                   .DeserializeObject<MessageContainer<MessagesInfo>>(e.Data);
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (MessageList.Children.Count == 0)
                    {


                        foreach (var item in concreteContainer.Body.messages.OrderBy(w => w.DepartureDate))
                        {
                            Add_Message(item);
                        }
                    }




                }));

            }
        }
        //websocket logic
        private void AcceptSocketMessages()
        {
            _webSocket.OnMessage += (sender, e) =>
                OnMessageHandler(sender, e);
            _webSocket.Connect();
            _webSocket.Send(JsonConvert.SerializeObject(new MessageContainer<string>() { MessageType = MessageTypes.AppMessage, Body = GroupId }));
            _webSocket.Send(JsonConvert.SerializeObject(new MessageContainer<KeyValuePair<string,string>>() { MessageType = MessageTypes.ToServerMessage, Body = new KeyValuePair<string, string>(GroupId,user) }));
        }

        private void Add_Message(MessageTo message)
        { 
         Dispatcher.BeginInvoke(new Action(() =>
            {
                if (message.From == user)
                {
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        MessageList.Children.Add(new Label() { Content = "From " + message.From + " : " + message.MessageText, HorizontalContentAlignment = HorizontalAlignment.Left });
                    }));
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        MessageList.Children.Add(new Label() { Content = "From " + message.From + " : " + message.MessageText, HorizontalContentAlignment = HorizontalAlignment.Right });
                    }));
                }




            }));
        }
        //Sending message
        private void send_Click(object sender, RoutedEventArgs e)
        {
            var mes = new MessageTo() { DepartureDate = DateTime.Now, From = user, MessageText = message.Text, To = GroupId };
          
            _webSocket.Send(JsonConvert.SerializeObject(new MessageContainer<MessageTo>() { MessageType = MessageTypes.NewIncomingMessage, Body = mes }));
        }
    }
}
