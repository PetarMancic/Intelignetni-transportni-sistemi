using Application.Features.Rating.Query;
using Core.Dto;
using Core.Repository_Interfaces;
using MediatR;

public class GetVehicleRatingsQueryHandler : IRequestHandler<GetVehicleRatingsQuery, VehicleRatingsDto>
{
    private readonly IRatingRepository _ratingRepository;

    public GetVehicleRatingsQueryHandler(IRatingRepository ratingRepository)
    {
        _ratingRepository = ratingRepository;
    }

    public async Task<VehicleRatingsDto> Handle(GetVehicleRatingsQuery request, CancellationToken cancellationToken)
    {
        var ratings = await _ratingRepository.GetRatingsByVehicleIdAsync(request.VehicleId, cancellationToken);

        var ratingItems = ratings.Select(r => new RatingItemDto
        {
            RideId = r.RideId,
            Value = r.Value,
            Comment = r.Comment,
            PassengerName = r.Passenger?.FirstName + " " + r.Passenger?.LastName,
            Date = r.Ride?.FinishedAt ?? r.Ride?.RequestedAt ?? DateTime.UtcNow
        }).ToList();

        return new VehicleRatingsDto
        {
            VehicleId = request.VehicleId,
            AverageRating = ratingItems.Any() ? Math.Round(ratingItems.Average(r => r.Value), 1) : 0,
            TotalRatings = ratingItems.Count,
            Ratings = ratingItems
        };
    }
}