using Core.Dto;
using MediatR;

namespace Application.Features.Passengers.Queries
{
    public sealed record LoginQuery(LoginRequestDto request) : IRequest<LoginResponseDto>;
   
}
