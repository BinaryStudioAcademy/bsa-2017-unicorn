namespace Unicorn.Shared.DTOs.Email
{
    public class EmailMessage
    {
        public string Body { get; set; }
        public bool IsHtml { get; set; }
        public string ReceiverEmail { get; set; }
        public string Subject { get; set; }
    }
}
