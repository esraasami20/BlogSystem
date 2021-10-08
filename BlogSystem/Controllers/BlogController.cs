using BlogSystem.Models;
using BlogSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public ActionResult<List<Blog>> getallBlogs()
        {
            return _blogService.GetBlogs();
        }

        //search blog
        [HttpGet("search/{name}")]
        public ActionResult<List<Blog>> getCatBysearch(string name)
        {
            return _blogService.GetBlogsBySearch(name);
        }


        //get blog by id
        [HttpGet("{BlogId}")]
        public ActionResult<Blog> getCatById(int BlogId)
        {
            return _blogService.GetBlog(BlogId);
        }

        //add blog
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Blog>> AddCategoryAsync([FromForm] Blog blog, IFormFile file)// not swagger [from form]
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                Blog result = await _blogService.AddBlog(blog, file);

                return Ok(result);
            }

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Moderator")]
        public async Task<ActionResult<Blog>> EditBlogAsync(int id, [FromForm] Blog blog, IFormFile file)// not swagger [from form]
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                blog.BlogId = id;
                var result = await _blogService.EditBlogAsync(blog, file);
                if (result != null)
                    return NoContent();
                return NotFound();
            }

        }



        // delete blog
        [HttpDelete("{id}")]
        public ActionResult<Blog> deleteBlog(int id)
        {

            var result = _blogService.DeleteBlog(id);
            if (result.Status == "Success")
                return NoContent();
            else if (result.Status == "Error")
                return new StatusCodeResult(StatusCodes.Status405MethodNotAllowed);
            else
                return NotFound();
        }


    }
}
