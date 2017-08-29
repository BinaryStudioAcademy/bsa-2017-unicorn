using System;

namespace Unicorn.Shared.DTOs.Chat
{
    public class ChatMessageDTO
    {
        public long DialogId { get; set; }
        public bool IsReaded { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public long OwnerId { get; set; }
    }
}
