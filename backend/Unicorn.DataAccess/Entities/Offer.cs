using Unicorn.DataAccess.Entities.Enum;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Offer : IEntity
    {
        public long Id { get; set; }

        public bool IsDeleted { get; set; }

        public string AttachedMessage { get; set; }

        public string DeclinedMessage { get; set; }

        public OfferStatus Status { get; set; }

        public virtual Company Company { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}
