using System.ComponentModel.DataAnnotations;

namespace AF.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int AuthorId { get; set; }
        public User? Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
