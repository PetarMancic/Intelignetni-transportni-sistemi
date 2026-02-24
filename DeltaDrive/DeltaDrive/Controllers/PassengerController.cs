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
        public async Task<TenNearestVehiclesResponseDto> GetTenNearestVehicles([FromQuery] TenNearestVehiclesRequestDto request)
        {          
            var stopwatch = Stopwatch.StartNew();
            var result = await _mediator.Send(
                    new GetTenNearestVehiclesQuery(request)); //ovde treba request 

            stopwatch.Stop(); 
            var elapsedMs = stopwatch.ElapsedMilliseconds;
           
            return result;
        }

        [HttpPost("BookVehicle")]
        public async Task<bool> BookVehicle(BookVehicleRequestDto vehicleDto)
        {

            var command = new BookVehicleCommand(vehicleDto.VehicleId,vehicleDto.PassengerId, vehicleDto.StartLat, vehicleDto.StartLon, vehicleDto.DestLat, vehicleDto.DestLon);
            return await _mediator.Send(command);
        }
    }
}
