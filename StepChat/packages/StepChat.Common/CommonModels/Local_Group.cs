using StepChat.Server.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace StepChat.Common.CommonModels
{
    public class Local_Group
    {
        public string Title { get; set; }

        public string GroupId;

        public List<User> Users;

        public GroupTypes GroupType;

        public enum GroupTypes
        {
           PersonToPerson,
           BigGroup
           
        }

    }
}
