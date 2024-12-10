using AF.Models;
using AF.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        // GET: api/article
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetAllArticles()
        {
            var articles = await _articleRepository.GetAllArticlesAsync();
            return Ok(articles);
        }

        // GET: api/article/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await _articleRepository.GetArticleByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }

        // POST: api/article
        [HttpPost]
        public async Task<ActionResult<Article>> CreateArticle([FromBody] Article article)
        {
            if (article == null)
            {
                return BadRequest("Article cannot be null.");
            }

            await _articleRepository.CreateArticleAsync(article);
            return CreatedAtAction(nameof(GetArticle), new { id = article.Id }, article);
        }

        // PUT: api/article/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle(int id, [FromBody] Article article)
        {
            if (id != article.Id)
            {
                return BadRequest("Article ID mismatch.");
            }

            await _articleRepository.UpdateArticleAsync(article);
            return NoContent();
        }

        // DELETE: api/article/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            await _articleRepository.DeleteArticleAsync(id);
            return NoContent();
        }
    }
}
