using Crud_Application.Data;
using Crud_Application.Models;
using Crud_Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crud_Application.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PostController : ControllerBase
    {
        private readonly IDataService _dataService;
        public PostController(IDataService dataService)
        {
            _dataService = dataService;
        }    

        [HttpPost]
        public async Task<ActionResult<Post>> AddPost(Post post)
        {
            var dbPost = await _dataService.AddPost(post);

            if (dbPost == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{post.Title} could not be added.");
            }

            return CreatedAtAction("GetPost", new { id = post.ID }, post);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost(int id, Post post)
        {
            if (id != post.ID)
            {
                return BadRequest();
            }

            Post dbPost = await _dataService.UpdatePost(post);

            if (dbPost == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{post.Title} could not be updated");
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _dataService.GetPostByID(id);
            (bool status, string message) = await _dataService.DeletePost(post);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, post);
        }


        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _dataService.GetPosts();
            if (posts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No posts in database.");
            }

            return StatusCode(StatusCodes.Status200OK, posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _dataService.GetPostByID(id);
            if (post == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No post found for id: {id}");
            }

            return StatusCode(StatusCodes.Status200OK, post);
        }
    }
}
