using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq.AutoMock;
using Xunit;
using System.Linq;
using Bogus;
using Booking.Business.Services;
using Booking.Business.Models;

namespace Booking.Tests.Config
{
    [CollectionDefinition(nameof(ManageBookingsCollection))]
    public class ManageBookingsCollection : ICollectionFixture<ManageBookingsTestsFixture> { }
    public class ManageBookingsTestsFixture : IDisposable
    {
        public RoomService RoomService;
        public ReservationService ReservationService;
        public AutoMocker AutoMocker;

        public IEnumerable<Room> GenerateRooms(int amount) 
        {
            var rooms = new Faker<Room>()
                .CustomInstantiator(r => new Room
                {
                    Id = Guid.NewGuid(),
                    Number = r.Random.Number(100, 9999),
                    Floor = r.Random.Number(1, 99),
                    Description = r.Lorem.Sentence(10),
                    Active = true,
                    Price = r.Random.Decimal(30, 999)
                });

            return rooms.Generate(amount);
        }

        public ReservationService GetReservationService() 
        {
            AutoMocker = new AutoMocker();
            ReservationService = AutoMocker.CreateInstance<ReservationService>();

            return ReservationService;
        }

        public RoomService GetRoomService()
        {
            AutoMocker = new AutoMocker();
            RoomService = AutoMocker.CreateInstance<RoomService>();

            return RoomService;
        }

        public void Dispose()
        {            
        }
    }
}
