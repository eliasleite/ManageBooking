using Booking.Business.Interfaces;
using Booking.Business.Models;
using Booking.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Data.Repository
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        public ReservationRepository(ManageBookingContext manageBookingContext) : base(manageBookingContext) { }

        public async Task<Reservation> GetReservation(Guid id)
        {
            return await _manageBookingContext.Reservations.AsNoTracking().Include(r => r.Room)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
