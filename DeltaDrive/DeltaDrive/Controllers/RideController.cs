using Application.Features.Rides.Queries;
using Core.Dto;
using DeltaDrive.Features.Passengers.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeltaDrive.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RideController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RideController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("HistoryOfPassenger'sRides")]
        public async Task<List<Ride>> GetHistoryOfPassengerRides(int passengerId)
        {
            var rides = await _mediator.Send(new GetHistoryOfPassengerRidesQuery(passengerId));
            return rides;
        }

        [HttpPost("BookRide")]
        public async Task<bool> BookRide([FromBody] BookVehicleRequestDto vehicleDto)
        {
            var command = new BookVehicleCommand(vehicleDto.VehicleId, vehicleDto.PassengerId, vehicleDto.StartLat, vehicleDto.StartLon, vehicleDto.DestLat, vehicleDto.DestLon);
            return await _mediator.Send(command);
        }
    }
}
