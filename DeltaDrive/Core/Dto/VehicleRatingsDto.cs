namespace Core.Dto
{
    public class VehicleRatingsDto
    {
        public int VehicleId { get; set; }
        public double AverageRating { get; set; }
        public int TotalRatings { get; set; }
        public List<RatingItemDto> Ratings { get; set; }
    }
}
