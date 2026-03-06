using MediatR;

namespace Application.Features.Passengers.Commands
{
   public record VerifyEmailCommand(string Token) : IRequest<bool>;
}
