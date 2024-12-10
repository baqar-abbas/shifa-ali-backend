using AF.Models;

namespace AF.Repositories
{
    // IArticleRepository.cs
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAllArticlesAsync();
        Task<Article> GetArticleByIdAsync(int id);
        Task CreateArticleAsync(Article article);
        Task UpdateArticleAsync(Article article);
        Task DeleteArticleAsync(int id);
    }
}
