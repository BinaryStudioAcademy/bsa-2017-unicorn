using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class SocialAccount : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public long FacebookUID { get; set; }
        public long GoogleUID { get; set; }
    }
}
