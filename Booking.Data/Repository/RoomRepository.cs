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
    public class RoomRepository: Repository<Room>, IRoomRepository
    {
        public RoomRepository(ManageBookingContext manageBookingContext) : base(manageBookingContext) { }

        public async Task<IEnumerable<Room>> GetRoomsWithReservations()
        {
            return await _manageBookingContext.Rooms.AsNoTracking().Include(r => r.Reservations).ToListAsync();
        }
    }
}
