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
        private readonly IRoomRepository _roomRepository;
        public ReservationService(IReservationRepository reservationRepository, IRoomRepository roomRepository, INotificator notificator) : base(notificator)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
        }

        public async Task Add(Reservation reservation)
        {
            if (!ExecuteValidation(new ReservationValidation(), reservation)) return;

            CalculateReservationPrice(reservation);

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

            CalculateReservationPrice(reservation);

            await _reservationRepository.Update(reservation);
        }

        public Reservation CalculateReservationPrice(Reservation reservation) 
        {
            var daysOfReservation = reservation.EndDate - reservation.StartDate;

            reservation.Room = _roomRepository.GetById(reservation.RoomId).Result;

            var price = reservation.Room.Price * daysOfReservation.Days;

            reservation.Price = price;

            return reservation;
        }
    }
}
