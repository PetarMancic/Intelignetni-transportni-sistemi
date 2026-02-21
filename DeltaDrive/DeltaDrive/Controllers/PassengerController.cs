using DeltaDrive.Features.Passengers.Commands;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using DeltaDrive.Features.Passengers.Queries;
using DeltaDrive.Dto;

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

        [HttpGet("GetClosestVehicles")]
        public async Task<FindTenNearestVehicles> GetClosestVehicles(double latitude, double longitude)
        {
            var closestVehicles = await _mediator.Send(new GetClosestVehiclesQuery(latitude, longitude));
            return closestVehicles;
        }
    }
}
