using Booking.Api.ViewModels;
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
    public class ReservationController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ReservationViewModel>> Add(ReservationViewModel reservationViewModel) 
        {
            if (!ModelState.IsValid) return BadRequest(reservationViewModel);
            return Ok();
        }
    }
}
