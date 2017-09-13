using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Shared.DTOs.Chart
{
    public class AnalyticsDTO
    {
        public LineChartDTO BooksAccepted { get; set; }

        public LineChartDTO BooksDeclined { get; set; }

        public PieChartDTO PopularWorks { get; set; }

        public PieChartDTO ConfirmedWorks { get; set; }

        public PieChartDTO DeclinedWorks { get; set; }
    }
}
