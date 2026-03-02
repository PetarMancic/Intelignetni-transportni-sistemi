namespace Core.Dto
{
    public sealed record LeaveRatingDto (int rideId, int passengerId, int grade, string comment);
}
