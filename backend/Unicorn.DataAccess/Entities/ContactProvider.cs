
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class ContactProvider : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public bool IsDeleted { get; set; }
    }
}
