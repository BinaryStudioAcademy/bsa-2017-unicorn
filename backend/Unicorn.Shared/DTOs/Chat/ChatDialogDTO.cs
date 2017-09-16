using System;
using System.Collections.Generic;

namespace Unicorn.Shared.DTOs.Chat
{  
    public class ChatDialogDTO
    {
        public long Id { get; set; }
        public long ParticipantOneId { get; set; }
        public long ParticipantTwoId { get; set; }
        public string ParticipantName { get; set; }
        public string ParticipantAvatar { get; set; }
        public string ParticipantType { get; set; }
        public long ParticipantProfileId { get; set; }
        public bool Participant1_Hided { get; set; }
        public bool Participant2_Hided { get; set; }
        public virtual ICollection<ChatMessageDTO> Messages { get; set; }
        public DateTimeOffset? LastMessageTime { get; set; }
        public bool? IsReadedLastMessage { get; set; }
    }
}
