using Application.Exceptions;
using Application.Service_Interfaces;
using Core.Dto;
using DeltaDrive.Repository.Interfaces;
using MediatR;

namespace Application.Features.Passengers.Queries
{
    public sealed class LoginQyeryHandler : IRequestHandler<LoginQuery, LoginResponseDto>
    {
        private readonly IPassengerRepository _passengerRepository;
        private readonly ITokenService _tokenService;

        public LoginQyeryHandler(IPassengerRepository passengerRepository, ITokenService tokenService)
        {
            _passengerRepository = passengerRepository;
            _tokenService = tokenService;
        }


        public async Task<LoginResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            // 1. Pronađi putnika po emailu
            var passenger = await _passengerRepository.GetByEmailAsync(request.request.Email);

            if (passenger is null)
                throw new NotFoundException("Pogrešan email ili lozinka.");

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.request.Password, passenger.PasswordHash);
            if (!isPasswordValid)
                throw new UnauthorizedAccessException("Pogrešan email ili lozinka.");

            if (!passenger.IsEmailVerified)
                throw new UnauthorizedAccessException("Email is not verified!");

            // 3. Generiši token
            var token = _tokenService.GenerateToken(passenger.Id, passenger.Email);

            // 4. Vrati response
            return new LoginResponseDto(
                passenger.Id,
                passenger.Email,
                passenger.FirstName,
                passenger.LastName,
                token
            );
        }
    }
}
