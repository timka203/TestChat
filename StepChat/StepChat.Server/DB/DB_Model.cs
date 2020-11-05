namespace StepChat.Server.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DB_Model : DbContext
    {
        public DB_Model()
            : base("name=DB_Model")
        {
        }

        public virtual DbSet<Chat_room> Chat_room { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<MessageTo> MessageTo { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
