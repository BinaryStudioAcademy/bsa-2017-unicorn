using Unicorn.Shared.DTOs;
using Unicorn.DataAccess.Entities;

namespace Unicorn.Core.Converters
{
    public static class WorkDTOConverter
    {
        public static WorkDTO WorkToDTO(Work work)
        {
            return new WorkDTO()
            {
                Id = work.Id,
                Description = work.Description,
                Name = work.Name,

                Category = work.Subcategory.Category.Name,
                Subcategory = work.Subcategory.Name
            };
        }
    }
}
