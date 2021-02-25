namespace StepChat.Server.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Group")]
    public partial class Group
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string GroupName { get; set; }

   
        [StringLength(255)]
        public string GroupType { get; set; }

     
        public string GroupAppNum { get; set; }


    }
}
