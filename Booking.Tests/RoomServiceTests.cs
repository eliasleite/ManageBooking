using Booking.Business.Interfaces;
using Booking.Business.Notifications;
using Booking.Business.Services;
using Booking.Tests.Config;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Booking.Tests
{
    [Collection(nameof(ManageBookingsCollection))]
    public class RoomServiceTests
    {
        private readonly ManageBookingsTestsFixture _manageBookingsTestsFixture;
        private readonly RoomService _roomService;

        public RoomServiceTests(ManageBookingsTestsFixture manageBookingsTestsFixture)
        {
            _manageBookingsTestsFixture = manageBookingsTestsFixture;
            _roomService = _manageBookingsTestsFixture.GetRoomService();
        }

        [Fact(DisplayName = "Add Room Successfully")]
        [Trait("Category", "Room Service Tests")]
        public void RoomService_Add_ShouldAddSuccessfully()
        {
            // Arrange 
            var room = _manageBookingsTestsFixture.GenerateValidRoom();
            _manageBookingsTestsFixture.AutoMocker.GetMock<IRoomRepository>().Setup(r => r.GetAll())
                .Returns(Task.FromResult(_manageBookingsTestsFixture.GetDistinctRooms(0)));

            // Act
            _roomService.Add(room).GetAwaiter().GetResult();

            // Assert
            _manageBookingsTestsFixture.AutoMocker.GetMock<IRoomRepository>().Verify(r => r.Add(room), Times.Once);
            _manageBookingsTestsFixture.AutoMocker.GetMock<INotificator>().Verify(r => r.Handle(It.IsAny<Notification>()), Times.Never);
        }

        [Fact(DisplayName = "Add Room Fail")]
        [Trait("Category", "Room Service Tests")]
        public void RoomService_Add_ShouldFail()
        {
            // Arrange 
            var room = _manageBookingsTestsFixture.GenerateInvalidRoom();

            // Act
            _roomService.Add(room).GetAwaiter().GetResult();

            // Assert
            _manageBookingsTestsFixture.AutoMocker.GetMock<IRoomRepository>().Verify(r => r.Add(room), Times.Never);
            _manageBookingsTestsFixture.AutoMocker.GetMock<INotificator>().Verify(r => r.Handle(It.IsAny<Notification>()), Times.AtLeastOnce);
        }

        [Fact(DisplayName = "Delete Room Successfully")]
        [Trait("Category", "Room Service Tests")]
        public void RoomService_Delete_ShouldDeleteSuccessfully()
        {
            // Arrange 
            var room = _manageBookingsTestsFixture.GenerateValidRoom();
            _manageBookingsTestsFixture.AutoMocker.GetMock<IRoomRepository>().Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(room));

            // Act
            _roomService.Delete(room.Id).GetAwaiter().GetResult();

            // Assert
            _manageBookingsTestsFixture.AutoMocker.GetMock<IRoomRepository>().Verify(r => r.Delete(room.Id), Times.Once);
            _manageBookingsTestsFixture.AutoMocker.GetMock<INotificator>().Verify(r => r.Handle(It.IsAny<Notification>()), Times.Never);
        }

        [Fact(DisplayName = "Delete Room Fail")]
        [Trait("Category", "Room Service Tests")]
        public void RoomService_Delete_ShouldFail()
        {
            // Arrange 
            var room = _manageBookingsTestsFixture.GenerateInvalidRoom();

            // Act
            _roomService.Delete(room.Id).GetAwaiter().GetResult();

            // Assert
            _manageBookingsTestsFixture.AutoMocker.GetMock<IRoomRepository>().Verify(r => r.Delete(room.Id), Times.Never);
            _manageBookingsTestsFixture.AutoMocker.GetMock<INotificator>().Verify(r => r.Handle(It.IsAny<Notification>()), Times.AtLeastOnce);
        }

        [Fact(DisplayName = "Update Room Successfully")]
        [Trait("Category", "Room Service Tests")]
        public void RoomService_Update_ShouldUpdateSuccessfully()
        {
            // Arrange 
            var room = _manageBookingsTestsFixture.GenerateValidRoom();
            _manageBookingsTestsFixture.AutoMocker.GetMock<IRoomRepository>().Setup(r => r.GetAll())
                .Returns(Task.FromResult(_manageBookingsTestsFixture.GetDistinctRooms(10)));

            // Act
            room.Description = "this is an update test.";
            _roomService.Update(room).GetAwaiter().GetResult();

            // Assert
            _manageBookingsTestsFixture.AutoMocker.GetMock<IRoomRepository>().Verify(r => r.Update(room), Times.Once);
            _manageBookingsTestsFixture.AutoMocker.GetMock<INotificator>().Verify(r => r.Handle(It.IsAny<Notification>()), Times.Never);
        }

        [Fact(DisplayName = "Update Room Fail")]
        [Trait("Category", "Room Service Tests")]
        public void RoomService_Update_ShouldFail()
        {
            // Arrange 
            var room = _manageBookingsTestsFixture.GenerateInvalidRoom();
            _manageBookingsTestsFixture.AutoMocker.GetMock<IRoomRepository>().Setup(r => r.GetAll())
                .Returns(Task.FromResult(_manageBookingsTestsFixture.GetDistinctRooms(10)));

            // Act
            room.Description = "this is an update test.";
            _roomService.Update(room).GetAwaiter().GetResult();

            // Assert
            _manageBookingsTestsFixture.AutoMocker.GetMock<IRoomRepository>().Verify(r => r.Update(room), Times.Never);
            _manageBookingsTestsFixture.AutoMocker.GetMock<INotificator>().Verify(r => r.Handle(It.IsAny<Notification>()), Times.AtLeastOnce);
        }

    }
}
