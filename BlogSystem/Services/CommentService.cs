using BlogSystem.DTO;
using BlogSystem.Helpers;
using BlogSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace BlogSystem.Services
{
    public class CommentService
    {
        private BlogContext _db;

        public CommentService(BlogContext db)
        {
            _db = db;
        }
        //get all unapproved comment
        public List<Comment> getUnApproved()
        {
            return _db.Comments.Where(a => a.IsAppeoved == false).Include(a=>a.Vistor).ThenInclude(a=>a.Comments).ThenInclude(a=>a.Blog).ThenInclude(a=>a.Comments).ToList();
        }
        //get all comments for spicific blog
        public Response GetApprivedCommentForSpicificBlog(int id)
        {
            var blog = _db.Blogs.FirstOrDefault(a => a.BlogId == id);
            if (blog != null)
                return new Response { Status = "Success", data = _db.Comments.Where(a => a.BlogId == blog.BlogId && a.IsAppeoved == true).ToList() };
            else
                return new Response { Status = "Error", Message = "Blog Not Found" };
        }

        //get not approved comments
        public Response GetNotApprivedCommentForSpicificBlog(int id)
        {
            var blog = _db.Blogs.FirstOrDefault(a => a.BlogId == id);
            if (blog != null)
                return new Response { Status = "Success", data = _db.Comments.Where(a => a.BlogId == blog.BlogId && a.IsAppeoved == false).ToList() };
            else
                return new Response { Status = "Error", Message = "Blog Not Found" };
        }
        

        // get all comments for spicific Vistor
        public Response GetNotApprovedCommentForBlog(int id, IIdentity vistor)
        {
            var vistorId = HelperMethods.GetAuthnticatedUserId(vistor);
            var blog = _db.Blogs.FirstOrDefault(a => a.BlogId == id);
            if (blog != null)
                return new Response { Status = "Success", data = _db.Comments.Where(a => a.BlogId == id && a.VistorId == vistorId && a.IsAppeoved == false).ToList() };
            else
                return new Response { Status = "Error", Message = "Comment Not Found" };
        }

        //get approved comment for vistor
        public Response GetApprovedCommentForBlog(int id, IIdentity vistor)
        {
            var vistorId = HelperMethods.GetAuthnticatedUserId(vistor);
            var blog = _db.Blogs.FirstOrDefault(a => a.BlogId == id);
            if (blog != null)
                return new Response { Status = "Success", data = _db.Comments.Where(a => a.BlogId == id && a.VistorId == vistorId && a.IsAppeoved == true).ToList() };
            else
                return new Response { Status = "Error", Message = "Comment Not Found" };
        }


        //add comment
        public Response AddNewComment(IIdentity vistor, Comment comment)
        {

            var vistorId = HelperMethods.GetAuthnticatedUserId(vistor);

            var blog = _db.Blogs.Include(r => r.Comments).FirstOrDefault(a => a.BlogId == comment.BlogId );
            if (blog != null)
            {
                if (blog.Comments.SingleOrDefault(pr => pr.VistorId == vistorId && pr.BlogId == comment.BlogId) == null)
                {
                    comment.VistorId = vistorId;
                    _db.Comments.Add(comment);
                    _db.SaveChanges();
                    UpdateBlogComment(comment.CommentId);
                    return new Response { Status = "Success", data = comment };
                }
            }
            return new Response { Status = "Error3", Message = "Product Not Found" };
        }


        //approve comment
        //public bool ApproveBlogComment(int id)
        //{

        //    Comment comment = _db.Comments.FirstOrDefault(a => a.CommentId == id);
        //    if (comment != null)
        //    {
        //        comment.IsAppeoved = true;
        //        _db.SaveChanges();
        //        return true;
        //    }
        //    return false;

        //}

        public Comment ApproveBlogComment(int id, ApproveComment approveComment)
        {
           // Moderator moderator = _db.Moderators.Include(r => r.ApplicationUser).FirstOrDefault(s => s.ModeratorId == HelperMethods.GetAuthnticatedUserId(user));
            //ApplicationUser admin = _db.Users.FirstOrDefault(s => s.Id == HelperMethods.GetAuthnticatedUserId(user));
            Comment comment = _db.Comments.FirstOrDefault(a => a.CommentId == id);
            if (comment != null)
            {
                comment.IsAppeoved = approveComment.IsAppeoved;
                comment.reason = approveComment.reason;
                _db.SaveChanges();
            }
            return comment;
        }

        //public Comment DisApproveBlogComment(int id, ApproveComment approveComment)
        //{
        //    // Moderator moderator = _db.Moderators.Include(r => r.ApplicationUser).FirstOrDefault(s => s.ModeratorId == HelperMethods.GetAuthnticatedUserId(user));
        //    //ApplicationUser admin = _db.Users.FirstOrDefault(s => s.Id == HelperMethods.GetAuthnticatedUserId(user));
        //    Comment comment = _db.Comments.FirstOrDefault(a => a.CommentId == id);
        //    if (comment != null)
        //    {
        //        comment.IsAppeoved = approveComment.IsAppeoved;
        //        comment.reason = approveComment.reason;
        //        _db.SaveChanges();
        //    }
        //    return comment;
        //}



        private void UpdateBlogComment(int blogId)
        {
            Blog blog = _db.Blogs.Include(r => r.Comments).SingleOrDefault(p => p.BlogId == blogId);
            var comments = blog.Comments.Select(s => s.BlogId);
            _db.SaveChanges();
        }


    }
}
