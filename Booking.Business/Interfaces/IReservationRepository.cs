using Booking.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Business.Interfaces
{
    public interface IReservationRepository: IRepository<Reservation>
    {
        Task<Reservation> GetReservation(Guid id);
    }
}
