using Booking.Business.Interfaces;
using Booking.Business.Models;
using Booking.Data.Context;
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
    }
}
