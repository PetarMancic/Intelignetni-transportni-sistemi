using MediatR;
using Microsoft.AspNetCore.Mvc;

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


        [HttpPost]
        public async Task<IActionResult> Create(CreateVehicleCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportFromCsv(ImportVehiclesFromCsvCommand command)
        {
            var command1= new ImportVehiclesFromCsvCommand("C:\\FAX\\MASTER\\Intelignetni-transportni-sistemi\\deltaTest.csv");
            await _mediator.Send(command1);
            return Ok();
        }
    }
}
