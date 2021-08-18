using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductMng.Contract;
using DataLayer.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductMng.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet, Route("getAll")]
        public async Task<List<Category>> getAll()
        {
            return await _categoryService.GetAll();
        }
        // GET category/5
        [HttpGet("specific/{id}")]
        public async Task<Category> Get(int id)
        {
            return await _categoryService.getCat(id);
        }

        
        
       
    }
}
