using BlogSystem.Helpers;
using BlogSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSystem.Services
{
    public class BlogService
    {
        private BlogContext _db;

        public BlogService(BlogContext db)
        {
            _db = db;
        }

        //get all blogs
        public List<Blog> GetBlogs()
        {
            return _db.Blogs.Include(a => a.Category).ThenInclude(a => a.Blogs).ThenInclude(a => a.Comments).ThenInclude(a=>a.Vistor).ThenInclude(a=>a.Comments).ThenInclude(a => a.Blog).ToList();
        }

        //get blog by id
        public Blog GetBlog(int id)
        {
            return _db.Blogs.Include(a => a.Category).ThenInclude(a => a.Blogs).ThenInclude(a=>a.Comments).FirstOrDefault(a => a.BlogId == id);
        }
        //search blog
        public List<Blog> GetBlogsBySearch(string name)
        {
            return _db.Blogs.Where(p => p.BlogName.Contains(name)).Include(a => a.Category).ThenInclude(a=>a.Blogs).ThenInclude(a=>a.Comments).ToList();
        }

        //add blog
        public async Task<Blog> AddBlog(Blog blog, IFormFile file)
        {
            _db.Blogs.Add(blog);
            _db.SaveChanges();
            if (file != null)
            {
                string imagepath = await FileHelper.SaveImageAsync(blog.BlogId, file, "Blogs");
                blog.Image = "http://localhost:25342/" + imagepath;
                _db.SaveChanges();
            }
            return blog;
        }

        //edit blog
        public async Task<Blog> EditBlogAsync(Blog blog, IFormFile file)
        {
            Blog blogDetails = GetBlog(blog.BlogId);
            if (blogDetails != null)
            {
                if (file != null)
                {
                    // delete old image
                    //File.Delete(blogDetails.Image);

                    // create new image
                    string imagepath = await FileHelper.SaveImageAsync(blogDetails.BlogId, file, "Blogs");
                    blogDetails.Image = imagepath;
                }
                blogDetails.BlogName = blog.BlogName;
                blogDetails.BlogBody = blog.BlogBody;
                blogDetails.CategoryId = blog.CategoryId;

                _db.SaveChanges();
            }
            return blogDetails;
        }

        //private int deleteImage(string path)
        //{
        //    //var filePath = System.Web.HttpContext.Current.Server.MapPath(path);
        //    var filePath = Microsoft.AspNetCore.Http.HttpContext.Current.Server.MapPath(path);
        //    if (File.Exists(filePath))
        //    {
        //        File.Delete(filePath);
        //    }
        //    return 1;
        //}

        //delete blog
        public Response DeleteBlog(int id)
        {
            Blog blog = GetBlog(id);
            if (blog != null)
            {
                _db.Blogs.Remove(blog);
                _db.SaveChanges();
                return new Response { Status = "Success", Message = "Blog Deleted successully" };
            }
            return new Response { Status = "Error2", Message = "Blog Not Found" };

        }


    }
}
