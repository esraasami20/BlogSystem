using BlogSystem.Helpers;
using BlogSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSystem.Services
{
    public class CategoryService
    {
        private BlogContext _db;

        public CategoryService(BlogContext db)
        {
            _db = db;
        }

        //search category
        public List<Category> GetCategoriesBySearch(string name)
        {
           return _db.Categories.Where(p => p.CategoryName.Contains(name)).Include(a=>a.Blogs).ToList();
        }

        //get all categories        
        public List<Category> GetCategories()
        {
            return _db.Categories.Include(a => a.Blogs).ToList();
        }

        //get category by id
        public Category GetCategory(int id)
        {
            return _db.Categories.Include(a => a.Blogs).FirstOrDefault(a => a.CategoryId == id);
        }

        //add categoey
        public Category AddCtegory(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            return category;
        }

        //edit category
        public Category EditCategory(Category category)
        {
            Category cat = GetCategory(category.CategoryId);
            if (cat != null)
            {
                cat.CategoryName = category.CategoryName;
                _db.SaveChanges();
                return category;
            }
            return null;
        }

        //delete Category
        public Response DeleteCategory(int id)
        {
            Category category = GetCategory(id);
            if (category != null)
            {
                _db.Remove(category);
                _db.SaveChanges();
                return new Response { Status = "Success", Message = "Category Deleted successully" };
            }
            return new Response { Status = "Error2", Message = "Category Not Found" };

        }





      


    }
}
