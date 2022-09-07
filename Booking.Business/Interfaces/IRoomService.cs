using Booking.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Business.Interfaces
{
    public interface IRoomService: IDisposable
    {
        Task Add(Room room);

        Task Update(Room room);

        Task Delete(Guid id);
    }
}
