namespace Core.Dto
{
    public class RatingItemDto
    {
        public int RideId { get; set; }
        public double Value { get; set; }
        public string? Comment { get; set; }
        public string PassengerName { get; set; }
        public DateTime Date { get; set; }
    }

}
