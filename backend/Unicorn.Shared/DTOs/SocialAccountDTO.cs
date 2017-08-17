namespace Unicorn.Shared.DTOs
{
    public class SocialAccountDTO
    {
        public long Id { get; set; }
        public AccountDTO Account { get; set; }
        public string Provider { get; set; }
        public string Uid { get; set; }
    }
}
