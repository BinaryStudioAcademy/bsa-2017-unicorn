using System;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class ChatMessage : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsReaded { get; set; }
        public ChatDialog Dialog { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public Account Owner { get; set; }
    }
}
