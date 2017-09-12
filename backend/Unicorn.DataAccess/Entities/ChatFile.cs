using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class ChatFile : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public string OriginalName { get; set; }
        public string ServerPathName { get; set; }        
    }
}
