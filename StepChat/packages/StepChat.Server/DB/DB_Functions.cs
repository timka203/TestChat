using StepChat.Common.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepChat.Server.DB
{
   public static class DB_Functions
    {
        private static DB_Model db = new DB_Model();
        public static void Add_User(string Login, string Password)
        {
            db.User.Add(new DB.User() { Password = Password, ContactNickname = Login });
            db.SaveChanges();

        }

        public static void Add_Message(MessageTo message)
        {
            db.MessageTo.Add(message);
            db.SaveChanges();
        }

        public static bool Find_User(string Login, string Password)
        {
            return db.User.Any(a => a.ContactNickname == Login && a.Password == Password);
        }

        public static List<MessageTo> GetMessages(string to)
        {
          return  db.MessageTo.Where(w =>  w.To == to).OrderBy(o => o.DepartureDate).ToList();
        }

        public static List<User> GetUsers()
        {
            return db.User.ToList();
        }

        public static List<Local_Group> GetGroups()
        {
            List<Local_Group> local_Groups = new List<Local_Group>();

            foreach (var item in db.Group)
            {
                List<User> users = new List<User>();
                foreach (var user in db.Chat_room.Where(e => e.Group_id == item.GroupAppNum).Select(s => s.User_id))
                {
                    users.Add(db.User.Where(w => w.Id == user).SingleOrDefault());
                }
                if (item.GroupType== "PersonToPerson")
                {
                    local_Groups.Add(new Local_Group() { Title = item.GroupName, Users = users, GroupId = item.GroupAppNum , GroupType= Local_Group.GroupTypes.PersonToPerson});

                }
                else if (item.GroupType == "BigGroup")
                {
                    local_Groups.Add(new Local_Group() { Title = item.GroupName, Users = users, GroupId = item.GroupAppNum, GroupType = Local_Group.GroupTypes.BigGroup });

                }



            }
            return local_Groups;
        }

        public static string Create_Group(List<User> users, string name, Local_Group.GroupTypes type)
        {
           

          
                string id = Guid.NewGuid().ToString("N");
            if (type == Local_Group.GroupTypes.PersonToPerson )
            {
                db.Group.Add(new Group() { GroupName = name, GroupAppNum = id, GroupType= "PersonToPerson" });

            }
            else if (type == Local_Group.GroupTypes.BigGroup)
            {
                db.Group.Add(new Group() { GroupName = name, GroupAppNum = id, GroupType= "BigGroup" });
            }
           
                db.SaveChanges();
                
                foreach (var user in users)
                {
                 db.Chat_room.Add(new Chat_room() { User_id=user.Id,Group_id=db.Group.Where(w=>w.GroupAppNum==id).SingleOrDefault().GroupAppNum});   
                }
               db.SaveChanges();


            return id;
        }

    }
}
