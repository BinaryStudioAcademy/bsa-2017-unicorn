namespace Unicorn.Shared.DTOs.Search
{
    public class SearchWorkDTO
    {
        public long Id { get; set; }

        public double Rating { get; set; }

        public int ReviewsCount { get; set; }

        public string Name { get; set; }

        public string Avatar { get; set; }

        public string PerformerType { get; set; }

        public string PerformerName { get; set; }

        public string Link { get; set; }

        public LocationDTO Location { get; set; }

        public double Distance { get; set; }
    }
}
