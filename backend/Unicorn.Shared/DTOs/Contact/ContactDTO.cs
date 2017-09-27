namespace Unicorn.Shared.DTOs.Contact
{
    public class ContactDTO
    {
        public long Id { get; set; }

        public ContactProviderDTO Provider { get; set; }

        public string Value { get; set; }
    }
}
