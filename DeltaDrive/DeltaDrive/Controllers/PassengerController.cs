using DeltaDrive.Features.Passengers.Commands;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using DeltaDrive.Features.Passengers.Queries;
using DeltaDrive.Dto;
using System.Diagnostics;

namespace DeltaDrive.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PassengerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PassengerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePassengerCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetPassengerByIdAsync(int passengerId)
        {
            var passenger = await _mediator.Send(new GetPassengedByIdQuery(passengerId));

            if (passenger is null)
                return NotFound();

            return Ok(passenger);
        }

        [HttpGet("GetTenNearestVehicles")]
        public async Task<TenNearestVehicles> GetTenNearestVehicles(double latitude, double longitude)
        {
            var stopwatch = Stopwatch.StartNew();  
            var closestVehicles = await _mediator.Send(new GetTenNearestVehiclesQuery(latitude, longitude));

            stopwatch.Stop(); 
            var elapsedMs = stopwatch.ElapsedMilliseconds;
           
            return closestVehicles;
        }
    }
}
