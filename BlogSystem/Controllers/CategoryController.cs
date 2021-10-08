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
    public class CategoryController : ControllerBase
    {
        CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        //get all Categories
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<Category>> getall()
        {
            return _categoryService.GetCategories();
        }

        //search category
        [HttpGet("search/{name}")]
        public ActionResult<List<Category>> getCatBysearch(string name)
        {
            return _categoryService.GetCategoriesBySearch(name);
        }


        //get category by id
        [HttpGet("{CategoryId}")]
        public ActionResult<Category> getCatById(int CategoryId)
        {
            return _categoryService.GetCategory(CategoryId);
        }

        //add new category
        [HttpPost]
        public ActionResult<Category> AddNewCategory(Category category)

        {
            if (ModelState.IsValid)
            {
                return Ok(_categoryService.AddCtegory(category));
            }
            return BadRequest();
        }

        //edit Category
        [HttpPut("{CategoryId}")]
        public ActionResult<Category> EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                return Ok(_categoryService.EditCategory(category));
            }
            return BadRequest();
        }

        //delete Category
        [HttpDelete("{CategoryId}")]
        public ActionResult<Category> EditCategory(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(_categoryService.DeleteCategory(id));
            }
            return BadRequest();
        }

    }
}
