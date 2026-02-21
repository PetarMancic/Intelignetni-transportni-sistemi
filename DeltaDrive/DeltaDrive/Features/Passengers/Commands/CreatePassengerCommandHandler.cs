using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DeltaDrive.Features.Passengers.Commands
{
    public sealed class CreatePassengerCommandHandler
        : IRequestHandler<CreatePassengerCommand, int>
    {
        private readonly DeltaDriveDbContext _context;
        private readonly IPasswordHasher<Passenger> _passwordHasher;

        public CreatePassengerCommandHandler(
            DeltaDriveDbContext context,
            IPasswordHasher<Passenger> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<int> Handle(
            CreatePassengerCommand request,
            CancellationToken cancellationToken)
        {
            var passenger = new Passenger
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth
            };

            passenger.PasswordHash =
                _passwordHasher.HashPassword(passenger, request.Password);

            _context.Passengers.Add(passenger);
            await _context.SaveChangesAsync(cancellationToken);

            return passenger.Id;
        }
    }
}
