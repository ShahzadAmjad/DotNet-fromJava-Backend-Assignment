using Backend_Assignment.Interfaces;
using Backend_Assignment.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend_Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPost _BlogPostService;
        public BlogPostController(IBlogPost blogPostService)
        {
            _BlogPostService = blogPostService;
        }
        // GET: api/<BlogPostController>
        [HttpGet("GetAll")]
        public async  Task<IActionResult> Get()
        {
            try
            {
                var model = await _BlogPostService.getAllBlogPosts();
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // GET api/<BlogPostController>/5
        [HttpGet("GetById")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var model = await _BlogPostService.getBlogPostById(id);
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // POST api/<BlogPostController>
        [HttpPost("Add")]
        public async Task<IActionResult> Add(BlogPost post)
        {
            try
            {
                var model = await _BlogPostService.CreateBlogPost(post);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // Post api to update record api/<BlogPostController>/5
        [HttpPost("Update")]
        public async Task<IActionResult> Update(BlogPost ModifiedBlogPostost)
        {
            try
            {
                var model = await _BlogPostService.updateBlogPost(ModifiedBlogPostost);
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // DELETE api/<BlogPostController>/5
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var model = await _BlogPostService.deleteBlogPost(id);
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
