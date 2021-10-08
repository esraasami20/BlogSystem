using BlogSystem.Helpers;
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
    public class CommentController : ControllerBase
    {
        private CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        //get all Approved comments for spicific blog
        [HttpGet("approved-comment/{BlogId}")]
        public ActionResult<Response> getAppCommById(int BlogId)
        {
            return _commentService.GetApprivedCommentForSpicificBlog(BlogId);
        }

        //get all Not Approved comments for spicific blog
        [HttpGet("notApp-comment/{BlogId}")]
        public ActionResult<Response> getNotAppCommById(int BlogId)
        {
            return _commentService.GetNotApprivedCommentForSpicificBlog(BlogId);
        }

        // get all Not app comments for spicific Vistor
        [HttpGet]
        [Route("vistorNotApp-comment/{id}")]
        public ActionResult<List<Comment>> getNotAppCommForVistor(int id)
        {

            Response reault = _commentService.GetNotApprovedCommentForBlog(id, User.Identity);
            if (reault.Status == "Success")
                return Ok(reault.data);
            return NotFound();
        }

        // get all  app comments for spicific Vistor
        [HttpGet]
        [Route("vistorApp-comment/{id}")]
        public ActionResult<List<Comment>> getAppCommentForVistor(int id)
        {

            Response reault = _commentService.GetApprovedCommentForBlog(id, User.Identity);
            if (reault.Status == "Success")
                return Ok(reault.data);
            return NotFound();
        }

        //add comment
        [HttpPost("{BlogId}")]
        [Authorize(Roles = "Vistor")]
        public ActionResult AddReviewToProduct(int BlogId, [FromBody] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.BlogId = BlogId;
                var result = _commentService.AddNewComment(User.Identity, comment);
                if (result.Status == "Success")
                {
                    return Ok(result.data);
                }
                else if (result.Status == "Error")
                {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
                else if (result.Status == "Error2")
                {
                    return new StatusCodeResult(StatusCodes.Status405MethodNotAllowed);

                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest(ModelState);
        }


        //approve Comment
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        //[Authorize(Roles = "Moderator")]
        public ActionResult approveComment(int id)
        {

            var result = _commentService.ApproveBlogComment(id);
            if (result)
                return NoContent();
            return NotFound();
        }
    }
}
