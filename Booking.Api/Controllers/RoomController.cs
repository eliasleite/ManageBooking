using AutoMapper;
using Booking.Api.ViewModels;
using Booking.Business.Interfaces;
using Booking.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : BaseController
    {
        private readonly IRoomService _roomService;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        public RoomController(
            INotificator notificator,
            IRoomRepository roomRepository,
            IRoomService roomService, 
            IMapper mapper) : base(notificator)
        {
            _roomService = roomService;
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<RoomViewModel>> GetAll() 
        {
            var rooms = _mapper.Map<IEnumerable<RoomViewModel>>(await _roomRepository.GetRoomsWithReservations());
            return rooms;
        }

        [HttpPost]
        public async Task<ActionResult<RoomViewModel>> Add(RoomViewModel roomViewModel) 
        {
            if (!ModelState.IsValid) return BadRequest(roomViewModel);

            var room = _mapper.Map<Room>(roomViewModel);

            await _roomService.Add(room);

            return CustomResponse(roomViewModel);
        }

        [HttpGet("{id:guid}")]
        public async Task<RoomViewModel> GetById(Guid id)
        {
            var room = _mapper.Map<RoomViewModel>(await _roomRepository.GetById(id));
            return room;
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<RoomViewModel>> Update(Guid id, RoomViewModel roomViewModel)
        {
            if (id != roomViewModel.Id) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(roomViewModel);

            var room = _mapper.Map<Room>(roomViewModel);

            await _roomService.Update(room);

            return CustomResponse(roomViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<RoomViewModel>> Delete(Guid id)
        {
            var room = await _roomRepository.GetById(id);

            if (room == null) return NotFound();

            await _roomService.Delete(id);

            return CustomResponse();
        }
    }
}
