using Core.Dto;
using MediatR;

namespace Application.Features.Rating.Query
{
    public sealed record GetVehicleRatingsQuery(int VehicleId) : IRequest<VehicleRatingsDto>;
}
