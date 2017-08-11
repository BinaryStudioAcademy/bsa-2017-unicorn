using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class SocialAccount : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public Account Account { get; set; }
        public string Provider { get; set; }
        public long Uid { get; set; }
    }
}
