namespace StepChat.Server.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string ContactNickname { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }
    }
}
