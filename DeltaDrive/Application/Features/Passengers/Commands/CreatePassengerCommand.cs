using MediatR;

namespace DeltaDrive.Features.Passengers.Commands
{
    public sealed record CreatePassengerCommand(
         string Email,
         string Password,
         string FirstName,
         string LastName,
         DateTime DateOfBirth
     ) : IRequest<int>;
}
