using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Booking.Api.ViewModels
{
    public class ReservationViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public Guid RoomId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public DateTime ReservationDate { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public DateTime EndDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public decimal Price { get; set; }

    }
}
