using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Popular;

namespace Unicorn.Core.Interfaces
{
    public interface IPopularService
    {
        Task<List<PopularCategoryDTO>> GetPopularCategories();
        Task<List<PerformerDTO>> GetPopularPerformers();
        Task<List<PerformerDTO>> GetPopularPerformers(long id);
        Task<List<FullPerformerDTO>> GetAllPerformersAsync();
        Task<List<FullPerformerDTO>> GetPerformersByFilterAsync(string city, string name);
    }
}
