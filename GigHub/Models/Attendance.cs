using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class Attendance
    {
        [Key]
        [Column(Order = 1)]
        public int GigId { get; set; }
        [Key]
        [Column(Order = 2)]
        public string AttendeeId { get; set; }
        public Gig Gig { get; set; }
        public ApplicationUser Attendee { get; set; }
    }
}