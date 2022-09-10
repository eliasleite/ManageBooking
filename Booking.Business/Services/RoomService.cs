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

            var rooms = await _roomRepository.GetAll();

            if (rooms.Count() > 0) 
            {
                Notificate("This Hotel has only on room available, you cannot insert another one.");
                return;                
            }           
            
            await _roomRepository.Add(room);
        }

        public async Task Delete(Guid id)
        {
            var room = await _roomRepository.GetById(id);
            if (room == null) 
            {
                Notificate("There is no room with this id");
                return;
            }

            await _roomRepository.Delete(id);
        }

        public void Dispose()
        {
            _roomRepository?.Dispose();
        }

        public async Task Update(Room room)
        {
            if (!ExecuteValidation(new RoomValidation(), room)) return;

            var rooms = _roomRepository.GetAll().Result;
            var anyEqualRoom = rooms.Where(r => r.Description == room.Description
             && r.Floor == room.Floor
             && r.Number == room.Number
             && r.Price == room.Price
            ).Any();

            if (anyEqualRoom) 
            {
                Notificate("There is already a room with this data");
                return;
            }

            await _roomRepository.Update(room);
        }
    }
}
