using System;
using System.Collections.Generic;

namespace BarWebApplication.Models
{
    public partial class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime DateAndTime { get; set; }
        public string LastName { get; set; }
        public int PeopleNo { get; set; }
    }
}
