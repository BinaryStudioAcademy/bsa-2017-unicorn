using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs.Chart;

namespace Unicorn.Core.Interfaces
{
    public interface IAnalyticsService
    {
        Task<AnalyticsDTO> GetCompanyAnalytics(long companyId);
        Task<AnalyticsDTO> GetVendorAnalytics(long vendorId);
    }
}
