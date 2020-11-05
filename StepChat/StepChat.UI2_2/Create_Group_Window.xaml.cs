using StepChat.Common.CommonModels;
using StepChat.Server.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StepChat.UI2_2
{
    /// <summary>
    /// Логика взаимодействия для Create_Group_Window.xaml
    /// </summary>
    public partial class Create_Group_Window : Window
    {
        private List<User> users;
        private List<User> group_users = new List<User>();

        public Create_Group_Window(List<User> users)
        {
            this.users = users;
            InitializeComponent();
        }

        private void AddToGroup_Click(object sender, RoutedEventArgs e)
        {
            ContactList.Items.Add(users.Where(w => w.Id == Convert.ToInt32(tbUserId.Text)));
            group_users.Add(users.Where(w => w.Id == Convert.ToInt32(tbUserId.Text)).FirstOrDefault());
        }

        private  void CreateGroup_Click(object sender, RoutedEventArgs e)
        {

            HTTP_SocketForUser.Create_Group(group_users, tbGroupName.Text, Local_Group.GroupTypes.BigGroup);
            this.Close();
        }
    }
}
