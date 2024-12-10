using System.ComponentModel.DataAnnotations;

namespace AF.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; } // This will store the hashed password

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? Username { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}