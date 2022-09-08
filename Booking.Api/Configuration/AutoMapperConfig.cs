using AutoMapper;
using Booking.Api.ViewModels;
using Booking.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Api.Configuration
{
    public class AutoMapperConfig: Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Room, RoomViewModel>().ReverseMap();
            CreateMap<Reservation, ReservationViewModel>().ReverseMap();
        }
    }
}
