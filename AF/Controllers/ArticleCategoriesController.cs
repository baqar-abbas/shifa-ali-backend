using AF.Models;
using AF.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleCategoriesController : ControllerBase
    {
        private readonly IArticleCategoriesRepository _articleCategoriesRepository;

        public ArticleCategoriesController(IArticleCategoriesRepository articleCategoriesRepository)
        {
            _articleCategoriesRepository = articleCategoriesRepository;
        }

        // GET: api/articlecategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleCategory>>> GetAllArticleCategories()
        {
            var categories = await _articleCategoriesRepository.GetAllArticleCategoriesAsync();
            return Ok(categories);
        }

        // GET: api/articlecategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleCategory>> GetArticleCategoryById(int id)
        {
            var category = await _articleCategoriesRepository.GetArticleCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST: api/articlecategories
        [HttpPost]
        public async Task<ActionResult<ArticleCategory>> CreateArticleCategory([FromBody] ArticleCategory category)
        {
            if (category == null)
            {
                return BadRequest("ArticleCategory cannot be null.");
            }

            await _articleCategoriesRepository.CreateArticleCategoryAsync(category);
            return CreatedAtAction(nameof(GetArticleCategoryById), new { id = category.Id }, category);
        }

        // PUT: api/articlecategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticleCategory(int id, [FromBody] ArticleCategory category)
        {
            if (id != category.Id)
            {
                return BadRequest("ArticleCategory ID mismatch.");
            }

            await _articleCategoriesRepository.UpdateArticleCategoryAsync(category);
            return NoContent();
        }

        // DELETE: api/articlecategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticleCategory(int id)
        {
            await _articleCategoriesRepository.DeleteArticleCategoryAsync(id);
            return NoContent();
        }
    }
}
