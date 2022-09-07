using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Api.ViewModels
{
    public class RoomViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required( ErrorMessage = "The field {0} is required")]
        public int Number { get; set; }


        [Required(ErrorMessage = "The field {0} is required")]
        public int Floor { get; set; }


        [Required(ErrorMessage = "The field {0} is required")]
        public decimal Price { get; set; }


        [StringLength(1000, ErrorMessage = "The field {0} needs to be between {2} and {1} characters", MinimumLength = 2)]
        public string Description { get; set; }

        public bool Active { get; set; }

        public IEnumerable<ReservationViewModel> Reservations { get; set; }

    }
}
