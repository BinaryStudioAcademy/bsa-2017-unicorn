using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Contact : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }

        public string Value { get; set; }

        public virtual ContactProvider Provider { get; set; }
        public virtual Account Account { get; set; }
    }
}
