using System.Collections.Generic;

namespace Unicorn.Shared.DTOs.Chat
{
    public class ChatDialogDTO
    {
        public long Id { get; set; }
        public long ParticipantOneId { get; set; }
        public long ParticipantTwoId { get; set; }
        public virtual ICollection<ChatMessageDTO> Messages { get; set; }
    }
}
