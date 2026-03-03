using DeltaDrive.Repository.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DeltaDrive.Features.Passengers.Commands
{
    public sealed class CreatePassengerCommandHandler
        : IRequestHandler<CreatePassengerCommand, int>
    {
        private readonly IPassengerRepository _passengerRepository;
        private readonly IPasswordHasher<Passenger> _passwordHasher;

        public CreatePassengerCommandHandler( //todo add repository injection
            IPassengerRepository passengerRepository,
            IPasswordHasher<Passenger> passwordHasher)
        {
            _passengerRepository = passengerRepository;
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

            passenger.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            await _passengerRepository.AddAsync(passenger);       

            return passenger.Id;
        }
    }
}
