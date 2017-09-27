using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Rating : IEntity
    {
        public long Id { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Account Reciever { get; set; }

        public virtual Account Sender { get; set; }

        public virtual Book Book { get; set; }

        public int Grade { get; set; }

    }
}
