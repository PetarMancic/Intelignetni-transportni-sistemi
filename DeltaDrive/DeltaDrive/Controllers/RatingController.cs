using Application.Features.Rating.Commands;
using Core.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeltaDrive.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly IMediator mediator;

        public RatingController( IMediator _mediator)
        {
            mediator = _mediator;   
        }

        [HttpPost("LeaveRateForARide")]
        public async Task<IActionResult> LeaveRating([FromBody] LeaveRatingDto request)
        {
            var command = new LeaveRatingCommand(request.rideId, request.passengerId, request.grade, request.comment);
            var result = await mediator.Send(command);

            return Ok(result);
        }
    }
}
