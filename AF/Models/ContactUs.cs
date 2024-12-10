using System.ComponentModel.DataAnnotations;

namespace AF.Models
{
    public class ContactUs
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? QueryType { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Location { get; set; }
    }
}
