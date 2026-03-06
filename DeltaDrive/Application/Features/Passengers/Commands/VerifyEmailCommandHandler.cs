using Application.Exceptions;
using DeltaDrive.Repository.Interfaces;
using MediatR;

namespace Application.Features.Passengers.Commands
{
    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, bool>
    {
        private readonly IPassengerRepository _passengerRepository;

        public VerifyEmailCommandHandler(IPassengerRepository passengerRepository)
            => _passengerRepository = passengerRepository;

        public async Task<bool> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var passenger = await _passengerRepository.GetByVerificationTokenAsync(request.Token);

            if (passenger is null)
                throw new NotFoundException("Nevažeći token.");

            if (passenger.EmailVerificationTokenExpiry < DateTime.UtcNow)
                throw new InvalidOperationException("Token je istekao.");

            if (passenger.IsEmailVerified)
                return true; // već verifikovan

            passenger.IsEmailVerified = true;
            passenger.EmailVerificationToken = null;
            passenger.EmailVerificationTokenExpiry = null;

            await _passengerRepository.UpdateAsync(passenger);
            return true;
        }
    }
}
