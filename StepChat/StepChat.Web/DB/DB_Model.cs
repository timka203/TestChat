namespace StepChat.Web.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using StepChat.Server.DB;

    public partial class DB_Model : DbContext
    {
        public DB_Model()
            : base("name=DB_Model")
        {
        }

        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
