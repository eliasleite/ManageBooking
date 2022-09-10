using Bogus;
using Booking.Business.Interfaces;
using Booking.Business.Models.Validations;
using Booking.Business.Notifications;
using Booking.Business.Services;
using Booking.Tests.Config;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Booking.Tests
{
    [Collection(nameof(ManageBookingsCollection))]
    public class ReservationServiceTests
    {
        private readonly ManageBookingsTestsFixture _manageBookingsTestsFixture;
        private readonly ReservationService _reservationService;
        public ReservationServiceTests(ManageBookingsTestsFixture manageBookingsTestsFixture)
        {
            _manageBookingsTestsFixture = manageBookingsTestsFixture;
            _reservationService = _manageBookingsTestsFixture.GetReservationService();
        }

        [Fact(DisplayName = "Add Reservation Successfully")]
        [Trait("Category", "reservation Service Tests")]
        public void ReservationService_Add_ShouldAddSuccessfully()
        {
            // Arrange 
            var reservation = _manageBookingsTestsFixture.GenerateValidReservation();

            // Act
            _reservationService.Add(reservation).GetAwaiter().GetResult();

            // Assert
            _manageBookingsTestsFixture.AutoMocker.GetMock<IReservationRepository>().Verify(r => r.Add(reservation), Times.Once);
            _manageBookingsTestsFixture.AutoMocker.GetMock<INotificator>().Verify(r => r.Handle(It.IsAny<Notification>()), Times.Never);
        }

        [Fact(DisplayName = "Add Reservation Fail")]
        [Trait("Category", "reservation Service Tests")]
        public void reservationService_Add_ShouldFail()
        {
            // Arrange 
            var reservation = _manageBookingsTestsFixture.GenerateInvalidReservation();

            // Act
            _reservationService.Add(reservation).GetAwaiter().GetResult();

            // Assert
            _manageBookingsTestsFixture.AutoMocker.GetMock<IReservationRepository>().Verify(r => r.Add(reservation), Times.Never);
            _manageBookingsTestsFixture.AutoMocker.GetMock<INotificator>().Verify(r => r.Handle(It.IsAny<Notification>()), Times.AtLeastOnce);
        }

        [Fact(DisplayName = "Delete Reservation Successfully")]
        [Trait("Category", "reservation Service Tests")]
        public void reservationService_Delete_ShouldDeleteSuccessfully()
        {
            // Arrange 
            var reservation = _manageBookingsTestsFixture.GenerateValidReservation();
            _manageBookingsTestsFixture.AutoMocker.GetMock<IReservationRepository>().Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(reservation));

            // Act
            _reservationService.Delete(reservation.Id).GetAwaiter().GetResult();

            // Assert
            _manageBookingsTestsFixture.AutoMocker.GetMock<IReservationRepository>().Verify(r => r.Delete(reservation.Id), Times.Once);
            _manageBookingsTestsFixture.AutoMocker.GetMock<INotificator>().Verify(r => r.Handle(It.IsAny<Notification>()), Times.Never);
        }

        [Fact(DisplayName = "Delete Reservation Fail")]
        [Trait("Category", "reservation Service Tests")]
        public void reservationService_Delete_ShouldFail()
        {
            // Arrange 
            var reservation = _manageBookingsTestsFixture.GenerateInvalidReservation();

            // Act
            _reservationService.Delete(reservation.Id).GetAwaiter().GetResult();

            // Assert
            _manageBookingsTestsFixture.AutoMocker.GetMock<IReservationRepository>().Verify(r => r.Delete(reservation.Id), Times.Never);
            _manageBookingsTestsFixture.AutoMocker.GetMock<INotificator>().Verify(r => r.Handle(It.IsAny<Notification>()), Times.AtLeastOnce);
        }

        [Fact(DisplayName = "Update Reservation Successfully")]
        [Trait("Category", "reservation Service Tests")]
        public void reservationService_Update_ShouldUpdateSuccessfully()
        {
            // Arrange 
            var reservation = _manageBookingsTestsFixture.GenerateValidReservation();
            _manageBookingsTestsFixture.AutoMocker.GetMock<IReservationRepository>().Setup(r => r.GetAll())
                .Returns(Task.FromResult(_manageBookingsTestsFixture.GetDistinctReservations(10)));

            // Act
            reservation.StartDate = reservation.StartDate.AddDays(1).AddHours(11);
            reservation.EndDate = reservation.EndDate.AddDays(1).AddHours(15);
            _reservationService.Update(reservation).GetAwaiter().GetResult();

            // Assert
            _manageBookingsTestsFixture.AutoMocker.GetMock<IReservationRepository>().Verify(r => r.Update(reservation), Times.Once);
            _manageBookingsTestsFixture.AutoMocker.GetMock<INotificator>().Verify(r => r.Handle(It.IsAny<Notification>()), Times.Never);
        }

        [Fact(DisplayName = "Update Reservation Fail")]
        [Trait("Category", "reservation Service Tests")]
        public void reservationService_Update_ShouldFail()
        {
            // Arrange 
            var reservation = _manageBookingsTestsFixture.GenerateInvalidReservation();
            _manageBookingsTestsFixture.AutoMocker.GetMock<IReservationRepository>().Setup(r => r.GetAll())
                .Returns(Task.FromResult(_manageBookingsTestsFixture.GetDistinctReservations(10)));

            // Act
            reservation.StartDate = DateTime.Now;
            _reservationService.Update(reservation).GetAwaiter().GetResult();

            // Assert
            _manageBookingsTestsFixture.AutoMocker.GetMock<IReservationRepository>().Verify(r => r.Update(reservation), Times.Never);
            _manageBookingsTestsFixture.AutoMocker.GetMock<INotificator>().Verify(r => r.Handle(It.IsAny<Notification>()), Times.AtLeastOnce);
        }
    }
}
