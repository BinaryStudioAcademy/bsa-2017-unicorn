using System;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class ChatMessage: IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        // public string Key { get; set; } // unique id?
    }
}
