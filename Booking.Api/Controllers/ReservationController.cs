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
    public class ReservationController : BaseController
    {
        private readonly IReservationService _reservationService;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public ReservationController(
            INotificator notificator,
            IReservationService reservationService,
            IReservationRepository reservationRepository,
            IMapper mapper) : base(notificator)
        {
            _reservationService = reservationService;
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ReservationViewModel>> Add(ReservationViewModel reservationViewModel) 
        {
            if (!ModelState.IsValid) return BadRequest(reservationViewModel);

            var reservation = _mapper.Map<Reservation>(reservationViewModel);

            await _reservationService.Add(reservation);

            return CustomResponse(reservationViewModel);
        }

        [HttpGet]
        public async Task<IEnumerable<ReservationViewModel>> GetAll()        
        {
            var reservations = _mapper.Map<IEnumerable<ReservationViewModel>>(await _reservationRepository.GetAll());
            return reservations;
        }

        [HttpGet("{id:guid}")]
        public async Task<ReservationViewModel> GetById(Guid id)
        {
            var reservation = _mapper.Map<ReservationViewModel>(await _reservationRepository.GetReservation(id));
            return reservation;
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ReservationViewModel>> Update(Guid id, ReservationViewModel reservationViewModel)
        {
            if (id != reservationViewModel.Id) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(reservationViewModel);

            var reservation = _mapper.Map<Reservation>(reservationViewModel);

            await _reservationService.Update(reservation);

            return CustomResponse(reservationViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ReservationViewModel>> Delete(Guid id) 
        {
            var reservation = await _reservationRepository.GetById(id);

            if (reservation == null) return NotFound();

            await _reservationService.Delete(id);

            return CustomResponse();
        }
    }
}
