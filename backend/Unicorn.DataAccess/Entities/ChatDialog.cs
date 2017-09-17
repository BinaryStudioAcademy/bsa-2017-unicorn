using System.Collections.Generic;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class ChatDialog : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Account Participant1 { get; set; }
        public virtual Account Participant2 { get; set; }
        public bool Participant1_Hided { get; set; }
        public bool Participant2_Hided { get; set; }
        public virtual ICollection<ChatMessage> Messages { get; set; }
    }
}
