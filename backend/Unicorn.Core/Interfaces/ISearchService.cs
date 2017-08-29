using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs.Search;

namespace Unicorn.Core.Interfaces
{
    public interface ISearchService
    {
        Task<List<SearchPerformerDTO>> GetSearchPerformers();
    }
}
