using Booking.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Business.Interfaces
{
    public interface IReservationService: IDisposable
    {
        Task Add(Reservation reservation);

        Task Update(Reservation reservation);

        Task Delete(Guid id);
    }
}
