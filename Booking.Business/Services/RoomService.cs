using Booking.Business.Interfaces;
using Booking.Business.Models;
using Booking.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Business.Services
{
    public class RoomService : BaseService, IRoomService
    {
        private readonly IRoomRepository _roomRepository;
           
        public RoomService(IRoomRepository roomRepository, INotificator notificator): base(notificator)
        {
            _roomRepository = roomRepository;
        }

        public async Task Add(Room room)
        {
            if (!ExecuteValidation(new RoomValidation(), room)) return;

            await _roomRepository.Add(room);
        }

        public async Task Delete(Guid id)
        {
            await _roomRepository.Delete(id);
        }

        public void Dispose()
        {
            _roomRepository?.Dispose();
        }

        public async Task Update(Room room)
        {
            if (!ExecuteValidation(new RoomValidation(), room)) return;

            await _roomRepository.Update(room);
        }
    }
}
