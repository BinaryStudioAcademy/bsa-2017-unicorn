using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Interfaces
{
    public interface IRatingService
    {
        Task<RatingDTO> GetByIdAsync(long id);
        Task<IEnumerable<RatingDTO>> GetByReceiverIdAsync(long id);
        Task<IEnumerable<RatingDTO>> GetBySenderIdAsync(long id);
        Task<double> GetAvarageByRecieverId(long id);
    }
}
