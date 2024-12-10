namespace AF.Models
{
    public class DonationCreateDto
    {
        public int FundraisingId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DonationDate { get; set; }
    }
}
