namespace StepChat.Server.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Chat_room
    {
        public int Id { get; set; }

        public int User_id { get; set; }

        public string Group_id { get; set; }
    }
}
