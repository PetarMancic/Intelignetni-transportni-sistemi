using MediatR;

namespace DeltaDrive.Features.Passengers.Queries
{
    public sealed record GetPassengedByIdQuery(int Id)
        : IRequest<Passenger?>;

}
