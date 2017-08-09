namespace Unicorn.DataAccess.Entities
{
    public class AccountSocialLogin
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public long FacebookUID { get; set; }
        public long GoogleUID { get; set; }
    }

    // Example:
    /*
     ID     Account     FacebookUID     GoogleUID
     1        1           123456          NULL
     2        2            NULL          987654
     3        3          123456789      987654321     
     */
}
