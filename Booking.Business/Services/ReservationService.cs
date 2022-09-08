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
    public class ReservationService : BaseService, IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        public ReservationService(IReservationRepository reservationRepository, INotificator notificator) : base(notificator)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task Add(Reservation reservation)
        {
            if (!ExecuteValidation(new ReservationValidation(), reservation)) return;

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
            if (!ExecuteValidation(new ReservationValidation(), reservation)) return;

            await _reservationRepository.Update(reservation);
        }
    }
}
