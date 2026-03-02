using MediatR;
namespace Application.Features.Rating.Commands
{
    public sealed record LeaveRatingCommand(int RideId,
        int PassengerId,
        double Value,
        string Comment) : IRequest<bool>;
}
