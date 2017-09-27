namespace Unicorn.Shared.DTOs
{
    public class RatingDTO
    {
        public long Id { get; set; }

        public AccountDTO Reciever { get; set; }

        public AccountDTO Sender { get; set; }

        public int Grade { get; set; }
    }
}
