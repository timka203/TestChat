using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StepChat.Common.CommonModels;
using StepChat.Server.DB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
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

namespace StepChat.UI2_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
         
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            // for .NET Core you need to add UseShellExecute = true
            // see https://docs.microsoft.com/dotnet/api/system.diagnostics.processstartinfo.useshellexecute#property-value
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            // set url for local web server
            string url;
            if (address.Text != "")
            {
                url = address.Text;
               
            }
            else {
               url="ws://127.0.0.1:15001";
                
            }
            WebSocketForUser.Start(url);

            //if (WebSocketForUser.IsValid(new Server.DB.User() { ContactNickname = login.Text, Password = Password.Password }))
            //{
            //    ChatWindow chatWindow = new ChatWindow(login.Text, url);
            //    chatWindow.Show();
            //    this.Close();
            //}
    

            var parsed = await HTTP_SocketForUser.Auth_UserAsync(login.Text, Password.Password);

           

            if (parsed!=null)
            {
                    WebSocketForUser.AuthUser( login.Text, parsed["Cookie"].ToString());
                    ChatWindow chatWindow = new ChatWindow(login.Text, url);
                    chatWindow.Show();
                    this.Close();
            }


            

        }
    }
}
