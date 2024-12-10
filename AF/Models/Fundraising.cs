using System.ComponentModel.DataAnnotations;

namespace AF.Models
{
    public class Fundraising
    {
        [Key]
        public int Id { get; set; }
        public string? Language { get; set; }
        public string? RaisingFundsFor { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public string? PatientContactNumber { get; set; }
    }
}
