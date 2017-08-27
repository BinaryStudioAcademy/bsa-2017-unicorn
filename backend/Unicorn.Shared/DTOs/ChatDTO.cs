using System;

namespace Unicorn.Shared.DTOs
{
    public class ChatDTO
    {
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}
