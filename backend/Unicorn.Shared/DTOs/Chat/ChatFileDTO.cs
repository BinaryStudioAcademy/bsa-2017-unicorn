namespace Unicorn.Shared.DTOs.Chat
{
    public class ChatFileDTO
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public string OriginalName { get; set; }
        public string ServerPathName { get; set; }        
    }
}
