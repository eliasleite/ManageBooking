using System;
using System.Collections.Generic;
using System.Linq;
using Moq.AutoMock;
using Xunit;
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

        public Room GenerateValidRoom() 
        {
            return GenerateRooms(1).FirstOrDefault();
        }

        public Reservation GenerateValidReservation()
        {
            return GenerateReservations(1).FirstOrDefault();
        }

        public Room GenerateInvalidRoom() 
        {
            var room = new Faker<Room>()
                    .CustomInstantiator(r => new Room
                    {
                        Id = Guid.NewGuid(),
                        Number = r.Random.Number(100, 9999),
                        Floor = r.Random.Number(1, 99),
                        Description = "",
                        Active = true,
                        Price = 0
                    });

            return room;
        }

        public Reservation GenerateInvalidReservation() 
        {
            var reservation = new Faker<Reservation>()
                    .CustomInstantiator(r => new Reservation
                    {
                        Id = Guid.NewGuid(),
                        RoomId = Guid.NewGuid(),
                        ReservationDate = DateTime.Now,
                        CheckInDate = DateTime.Now,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                        Price = 0
                    });

            return reservation;
        }

        public List<Reservation> GetDistinctReservations(int amount)
        {
            var reservations = new List<Reservation>();

            reservations.AddRange(GenerateReservations(amount).ToList());

            return reservations;
        }

        public List<Room> GetDistinctRooms(int amount) 
        {
            var rooms = new List<Room>();

            rooms.AddRange(GenerateRooms(amount).ToList());

            return rooms;
        }

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

        public IEnumerable<Reservation> GenerateReservations(int amount) 
        {
            var reservations = new Faker<Reservation>()
                .CustomInstantiator(r => new Reservation
                {
                    Id = Guid.NewGuid(),
                    RoomId = Guid.NewGuid(),
                    ReservationDate = DateTime.Now,               
                    StartDate = r.Date.Soon(2, DateTime.Now.AddDays(1).AddHours(1)),
                    EndDate = r.Date.Soon(3, DateTime.Now.AddDays(3)),
                    Price = r.Random.Decimal(30, 999)
                });

            return reservations.Generate(amount);
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
