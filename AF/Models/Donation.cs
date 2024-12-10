using System.ComponentModel.DataAnnotations;

namespace AF.Models
{
    public class Donation
    {
        [Key]
        public int Id { get; set; }
        public int FundraisingId { get; set; }
        public Fundraising? Fundraising { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public decimal Amount { get; set; }
        public DateTime DonationDate { get; set; }
    }
}
