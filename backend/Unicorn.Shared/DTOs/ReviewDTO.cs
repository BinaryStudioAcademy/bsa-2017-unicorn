namespace Unicorn.Core.DTOs
{
    public class ReviewDTO
    {
        public long Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public double Grade { get; set; }

        public string Description { get; set; }
    }
}
