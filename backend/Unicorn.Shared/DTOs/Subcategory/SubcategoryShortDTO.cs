namespace Unicorn.Shared.DTOs.Subcategory
{
    public class SubcategoryShortDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }

        public long CategoryId { get; set; }

        public string Category { get; set; }

        public string Icon { get; set; }
    }
}
