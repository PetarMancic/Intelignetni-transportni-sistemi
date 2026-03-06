using Application.Service_Interfaces;
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
        private readonly IEmailService _emailService;

        public CreatePassengerCommandHandler( //todo add repository injection
            IPassengerRepository passengerRepository,
            IPasswordHasher<Passenger> passwordHasher,
            IEmailService emailService)
        {
            _passengerRepository = passengerRepository;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
        }

        public async Task<int> Handle(
            CreatePassengerCommand request,
            CancellationToken cancellationToken)
        {

            var isPassengerExists = _passengerRepository.GetByEmailAsync(request.Email);
            if(isPassengerExists is not  null)
            {
                throw new InvalidOperationException("Korisnik sa ovim emailom već postoji.");
            }

            var verificationToken = Guid.NewGuid().ToString("N"); // random token


            var passenger = new Passenger
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                IsEmailVerified = false,
                EmailVerificationToken = verificationToken,
                EmailVerificationTokenExpiry = DateTime.UtcNow.AddHours(24)
            };

            passenger.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            await _passengerRepository.AddAsync(passenger);

            await _emailService.SendVerificationEmailAsync(passenger.Email, passenger.FirstName, verificationToken);

            return passenger.Id;
        }
    }
}
