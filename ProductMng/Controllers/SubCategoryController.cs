using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductMng.Contract;
using DataLayer.Models;

namespace ProductMng.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpGet, Route("getAll")]
        public async Task<List<SubCategory>> getAll()
        {
            return await _subCategoryService.GetAll();
        }
        // GET category/5
        [HttpGet("specific/{id}")]
        public async Task<SubCategory> Get(int id)
        {
            return await _subCategoryService.getSubCat(id);
        }
        [HttpGet("name/{id}")]
        public string GetName(int id)
        {
            return _subCategoryService.getName(id);
        }

        [HttpGet("getId/{name}")]
        public int GetId(string name)
        {
            return _subCategoryService.getId(name);
        }



    }
}
