using AF.Models;

namespace AF.Repositories
{
    public interface IArticleCategoriesRepository
    {
        Task<IEnumerable<ArticleCategory>> GetAllArticleCategoriesAsync();
        Task<ArticleCategory> GetArticleCategoryByIdAsync(int id);
        Task CreateArticleCategoryAsync(ArticleCategory category);
        Task UpdateArticleCategoryAsync(ArticleCategory category);
        Task DeleteArticleCategoryAsync(int id);
    }
}
