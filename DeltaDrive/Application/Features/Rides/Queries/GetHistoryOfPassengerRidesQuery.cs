using MediatR;

namespace Application.Features.Rides.Queries
{
    public sealed record GetHistoryOfPassengerRidesQuery(int passengerId) : IRequest<List<Ride>>;
}
