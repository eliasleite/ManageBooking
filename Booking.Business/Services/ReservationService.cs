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

            var isValid = await ValidateAddReservationAsync(reservation);
            if (!isValid)             
            {
                Notificate("There is already a reservation with this start date or end date");
                return;
            }

            await _reservationRepository.Add(reservation);
        }

        public async Task Delete(Guid id)
        {
            var reservation = await _reservationRepository.GetById(id);
            if (reservation == null)
            {
                Notificate("There is no reservation with this id");
                return;
            }

            await _reservationRepository.Delete(id);
        }

        public void Dispose()
        {
            _reservationRepository?.Dispose();
        }

        public async Task Update(Reservation reservation)
        {
            if (!ExecuteValidation(new ReservationValidation(), reservation)) return;

            var reservations = _reservationRepository.GetAll().Result;

            var anyEqualReservation = reservations.Where(r => r.StartDate == reservation.StartDate
            && r.EndDate == reservation.EndDate 
            && r.ReservationDate == reservation.ReservationDate 
            && r.RoomId == reservation.RoomId).Any();

            if (anyEqualReservation)
            {
                Notificate("There is already a reservation with this data");
                return;
            }            

            await _reservationRepository.Update(reservation);
        }

        private async Task<bool> ValidateAddReservationAsync(Reservation reservation) 
        {
            var reservations = await _reservationRepository.GetAll();
            if (!ValidateAnyEqualDate(reservation, reservations) ||
                !ValidateAnyBeforeAndBetweenDate(reservation, reservations) ||
                !ValidateAnyBetweenAndAfterDate(reservation, reservations) ||
                !ValidateAnyBetweenDate(reservation, reservations) ||
                !ValidateAnyOutsideDate(reservation, reservations)) 
            {
                return false;
            }
            return true;
        }

        private bool ValidateAnyEqualDate(Reservation reservation, List<Reservation> reservations) 
        {
            var anyEqual = reservations.Any(x => x.StartDate == reservation.StartDate && x.EndDate == reservation.EndDate);
            return anyEqual == true ? false : true;
        }

        private bool ValidateAnyBeforeAndBetweenDate(Reservation reservation, List<Reservation> reservations)
        {
            var anyEqual = reservations.Any(x => reservation.StartDate < x.StartDate && reservation.EndDate >= x.StartDate);
            return anyEqual == true ? false : true;
        }

        private bool ValidateAnyBetweenAndAfterDate(Reservation reservation, List<Reservation> reservations)
        {
            var anyEqual = reservations.Any(x => reservation.StartDate <= x.EndDate && reservation.EndDate >= x.EndDate);
            return anyEqual == true ? false : true;
        }

        private bool ValidateAnyBetweenDate(Reservation reservation, List<Reservation> reservations)
        {
            var anyEqual = reservations.Any(x => reservation.StartDate > x.StartDate && reservation.EndDate < x.EndDate);
            return anyEqual == true ? false : true;
        }

        private bool ValidateAnyOutsideDate(Reservation reservation, List<Reservation> reservations)
        {
            var anyEqual = reservations.Any(x => reservation.StartDate < x.StartDate && reservation.EndDate > x.EndDate);
            return anyEqual == true ? false : true;
        }
    }
}
