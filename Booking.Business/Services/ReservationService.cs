using Booking.Business.Interfaces;
using Booking.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Business.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task Add(Reservation reservation)
        {
            await _reservationRepository.Add(reservation);
        }

        public async Task Delete(Guid id)
        {
            await _reservationRepository.Delete(id);
        }

        public void Dispose()
        {
            _reservationRepository?.Dispose();
        }

        public async Task Update(Reservation reservation)
        {
            await _reservationRepository.Update(reservation);
        }
    }
}
