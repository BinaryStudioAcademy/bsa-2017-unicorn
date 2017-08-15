using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Review : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }


        public long FromAccountId { get; set; }
        public string From { get; set; }

        public long ToAccountId { get; set; }
        public string To {get; set; }

        public double Grade { get; set; }

        public string Description { get; set; }

        public long BookId { get; set; }
    }
}
