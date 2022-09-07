using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Business.Models
{
    public class Room
    {
        public Guid Id { get; set; }        
        public int Number { get; set; }        
        public int Floor { get; set; }        
        public decimal Price { get; set; }        
        public string Description { get; set; }
        public bool Active { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
    }
}
