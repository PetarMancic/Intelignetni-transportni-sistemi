using Application.Features.Passengers.Commands;
using Application.Features.Passengers.Queries;
using Core.Dto;
using Infrastructure.Services_Implementations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeltaDrive.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IMediator _mediator;   

        public AuthController( IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            // Proveri korisnika u bazi
            var user = await _mediator.Send(new LoginQuery(request));
            if (user is null)
                return Unauthorized("Pogrešan email ili lozinka.");

            return Ok(new { user });
        }

        // Controller
        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            await _mediator.Send(new VerifyEmailCommand(token));

            return Redirect("https://intelignetni-transportni-sistemi-production.up.railway.app/api/Auth/login");
            //return Redirect("http://localhost:8080/login");

        }

    }
}
