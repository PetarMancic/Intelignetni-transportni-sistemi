using Core.Dto;
using DeltaDrive.Features.Passengers.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DeltaDrive.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {

        private readonly IMediator _mediator;

        public VehicleController(IMediator mediator)
        {
            _mediator = mediator;
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


        [HttpPost]
        public async Task<IActionResult> CreateVehicle(CreateVehicleCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportFromCsv(ImportVehiclesFromCsvCommand command)
        {
            var command1= new ImportVehiclesFromCsvCommand("C:\\FAX\\MASTER\\Intelignetni-transportni-sistemi\\delta.csv");
            await _mediator.Send(command1);
            return Ok();
        }
    }
}
