using System;
using System.Collections.Generic;

namespace Unicorn.Shared.DTOs.Chat
{
    public class ChatMessageDTO
    {
        public long MessageId { get; set; }
        public long DialogId { get; set; }
        public bool IsReaded { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Message { get; set; }
        public long OwnerId { get; set; }
        public ICollection<ChatFileDTO> Files;
    }
}
