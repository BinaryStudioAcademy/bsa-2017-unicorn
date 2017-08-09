using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Review : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }

        public string From { get; set; }

        public string To {get; set; }

        public double Grade { get; set; }

        public string Description { get; set; }
    }
}
