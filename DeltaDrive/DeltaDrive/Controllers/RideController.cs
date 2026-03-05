using Application.Features.Rides.Queries;
using Core.Dto;
using DeltaDrive.Features.Passengers.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeltaDrive.Controllers
{
   
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

        [HttpGet("GetTenNearestVehicles")]
        public async Task<TenNearestVehiclesResponseDto> GetTenNearestVehicles([FromQuery] TenNearestVehiclesRequestDto request)
        {
           
            var result = await _mediator.Send(
                    new GetTenNearestVehiclesQuery(request)); //ovde treba request 

            return result;
        }



        [HttpPost("BookRide")]
        public async Task<IActionResult> BookRide([FromBody] BookVehicleRequestDto vehicleDto)
        {
            var command = new BookVehicleCommand(vehicleDto.VehicleId, vehicleDto.PassengerId, vehicleDto.StartLat, vehicleDto.StartLon, vehicleDto.DestLat, vehicleDto.DestLon);
            var rideId=  await _mediator.Send(command);
            return Ok(new{rideId});
        }
    }
}
