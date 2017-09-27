namespace Unicorn.Shared.DTOs.Chart
{
    public class AnalyticsDTO
    {
        public LineChartDTO BooksAccepted { get; set; }

        public LineChartDTO BooksDeclined { get; set; }

        public PieChartDTO PopularWorks { get; set; }

        public PieChartDTO ConfirmedWorks { get; set; }

        public PieChartDTO DeclinedWorks { get; set; }

        public PieChartDTO VendorsByRating { get; set; }

        public PieChartDTO VendorsByOrders { get; set; }

        public PieChartDTO VendorsByFinished { get; set; }
    }
}
