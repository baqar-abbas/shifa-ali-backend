using AF.Data;
using AF.Models;
using Microsoft.EntityFrameworkCore;

namespace AF.Repositories
{
    public class ArticleCategoriesRepository : IArticleCategoriesRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticleCategoriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ArticleCategory>> GetAllArticleCategoriesAsync()
        {
            return await _context.ArticleCategories.ToListAsync();
        }

        public async Task<ArticleCategory> GetArticleCategoryByIdAsync(int id)
        {
            return await _context.ArticleCategories.FindAsync(id);
        }

        public async Task CreateArticleCategoryAsync(ArticleCategory category)
        {
            await _context.ArticleCategories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateArticleCategoryAsync(ArticleCategory category)
        {
            _context.ArticleCategories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteArticleCategoryAsync(int id)
        {
            var category = await GetArticleCategoryByIdAsync(id);
            if (category != null)
            {
                _context.ArticleCategories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
