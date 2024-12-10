namespace AF.Models
{
    public class ArticleCategory
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
