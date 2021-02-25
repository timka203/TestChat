namespace StepChat.Server.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MessageTo")]
    public partial class MessageTo
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string From { get; set; }

        [Required]
        [StringLength(255)]
        public string To { get; set; }

        public string MessageText { get; set; }

        public DateTime DepartureDate { get; set; }
    }
}
