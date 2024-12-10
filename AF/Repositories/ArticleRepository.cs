using AF.Models;
using AF.Data;
using Microsoft.EntityFrameworkCore;

namespace AF.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all articles
        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            return await _context.Article.ToListAsync();
        }

        // Get article by ID
        public async Task<Article> GetArticleByIdAsync(int id)
        {
            return await _context.Article.FirstOrDefaultAsync(a => a.Id == id);
        }

        // Create a new article
        public async Task CreateArticleAsync(Article article)
        {
            await _context.Article.AddAsync(article);
            await _context.SaveChangesAsync();
        }

        // Update an existing article
        public async Task UpdateArticleAsync(Article article)
        {
            _context.Article.Update(article);
            await _context.SaveChangesAsync();
        }

        // Delete an article by ID
        public async Task DeleteArticleAsync(int id)
        {
            var article = await GetArticleByIdAsync(id);
            if (article != null)
            {
                _context.Article.Remove(article);
                await _context.SaveChangesAsync();
            }
        }
    }
}
