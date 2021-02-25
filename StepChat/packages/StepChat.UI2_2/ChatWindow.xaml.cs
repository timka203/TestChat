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
using System.Windows.Shapes;
using WebSocketSharp;

namespace StepChat.UI2_2
{
    /// <summary>
    /// Логика взаимодействия для ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        private WebSocket _webSocket;

        private string username;
        private User user ;
        string url;
        List<Local_Group> local_Groups;
        List<User> users= new List<User>();

        public ChatWindow(string user,string url)
        {
            this.username = user;
            this.url = url;
            _webSocket = new WebSocket(url);
            

            AcceptSocketMessages();
            InitializeComponent();

        }

        private void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var containerMetainfo = JsonConvert
                .DeserializeObject<MessageContainer<object>>(e.Data);

            if (containerMetainfo.MessageType == MessageTypes.ContactListStatusUpdate)
            {
                var concreteContainer = JsonConvert
                    .DeserializeObject<MessageContainer<List<ContactItemModel>>>(e.Data);
                
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    ContactList.Items.Clear();
                    var tmp = concreteContainer.Body.Where(w => w.ContactNickname == username).SingleOrDefault();
                    user = new User() { ContactNickname = tmp.ContactNickname, Id = tmp.ApplicationUserId };
                    lbId.Text=user.Id.ToString();

                    foreach (var item in concreteContainer.Body)
                    {
                        ContactList.Items.Add(item);
                        users.Add(new User() { Id = item.ApplicationUserId, ContactNickname = item.ContactNickname });
                        // AddContactItemToContactList( item, isInvokedFromNonUiThread: true);
                    }
                }));
            }
            if (containerMetainfo.MessageType == MessageTypes.GroupListUpdate)
            {
                var concreteContainer = JsonConvert
                    .DeserializeObject<MessageContainer<List<Local_Group>>>(e.Data);
                local_Groups = concreteContainer.Body;

                Dispatcher.BeginInvoke(new Action(() =>
                {
                    GroupList.ItemsSource=null;



                    GroupList.ItemsSource = concreteContainer.Body;
                    // AddContactItemToContactList( item, isInvokedFromNonUiThread: true);
                   
                }));
            }
        }
        private void AcceptSocketMessages()
        {
            _webSocket.OnMessage += (sender, e) =>
                OnMessageHandler(sender, e);
            _webSocket.Connect();
            _webSocket.Send(JsonConvert.SerializeObject(new MessageContainer<string>() { MessageType = MessageTypes.AppMessage, Body = "ChatWindow" }));

            Thread.Sleep(TimeSpan.FromSeconds(2));

                    
        }

        private async void ContactList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        { string id;
         ContactItemModel model= ((sender as ListView).SelectedItems[0]) as ContactItemModel;
            try
            {
                 id = local_Groups.Where(w => w.GroupType== Local_Group.GroupTypes.PersonToPerson && ((w.Users[0].ContactNickname==model.ContactNickname&& w.Users[1].ContactNickname == username)|| (w.Users[1].ContactNickname == model.ContactNickname && w.Users[0].ContactNickname == username))).SingleOrDefault().GroupId;
            }
            catch 
            {
                var parsed = await HTTP_SocketForUser.Create_Group(new List<User>() { user, new User() { Id = model.ApplicationUserId, ContactNickname = model.ContactNickname } }, model.ContactNickname + username,Local_Group.GroupTypes.PersonToPerson);
                id = parsed["Id"].ToString();
            }
            await Dispatcher.BeginInvoke(new Action(() =>
            {
                Chats.Items.Add(new TabItem() { Content = new Frame() { Content = new ChatPage(username, id, url) }, Header = model.ContactNickname });
            }));
        }


        private async void GroupList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string id;
            Local_Group model = ((sender as ListView).SelectedItems[0]) as Local_Group;
            id = model.GroupId;
          
            await Dispatcher.BeginInvoke(new Action(() =>
            {
                Chats.Items.Add(new TabItem() { Content = new Frame() { Content = new ChatPage(username, id, url) }, Header = model.Title });
            }));

        }

        private void btCreate_Click(object sender, RoutedEventArgs e)
        {
            Create_Group_Window create_Group_Window = new Create_Group_Window(users);
            create_Group_Window.Show();

        }

        //private void AddContactItemToContactList(

        //    ContactItemModel model,
        //    bool isInvokedFromNonUiThread = false)
        //{
        //    if (ContactList == null)
        //        throw new ArgumentNullException(nameof(ContactList));

        //    if (!isInvokedFromNonUiThread)
        //    {
        //        var items = ContactList.Items;
        //        items.Add(new ListViewItem()
        //        {
        //            Text = model.DisplayText,
        //            Tag = model.ApplicationUserId
        //        });
        //    }
        //    else
        //    {
        //        contactList.Invoke(new MethodInvoker(() =>
        //        {
        //            var items = contactList.Items;
        //            items.Add(new ListViewItem()
        //            {
        //                Text = model.DisplayText,
        //                Tag = model.ApplicationUserId
        //            });
        //        }));
        //    }

        //}
    }
}
