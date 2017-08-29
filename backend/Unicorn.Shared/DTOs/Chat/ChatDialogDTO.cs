using System.Collections.Generic;

namespace Unicorn.Shared.DTOs.Chat
{
    public class Participant
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class ChatDialogDTO
    {
        public long Id { get; set; }
        public Participant ParticipantOne { get; set; }
        public Participant ParticipantTwo { get; set; }
        public virtual ICollection<ChatMessageDTO> Messages { get; set; }
    }
}
