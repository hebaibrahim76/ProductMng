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
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
       

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet, Route("getAll")]
        public async Task<List<Product>> getAll()
        {
            return await _productService.GetAll();
        }
        // GET products/5
        [HttpGet("specific/{id}")]
        public async Task<Product> Get(int id)
        {
            return await _productService.getPr(id);
        }

        [HttpPost, Route("post")]

        public async Task<JsonResult> post([FromBody] Product pr)
        {
            await _productService.AddPr(pr);
            return Json(new { success = true, result = pr });
        }
        [HttpDelete, Route("delete/{id:int}")]
        public async Task<JsonResult> delete([FromRoute(Name = "id")] int id)
        {
            Product tobedeleted =await _productService.getPr(id);
            if (tobedeleted != null)
            {
                await _productService.DeletePr(tobedeleted);
                return Json(new { success = true, message = "the below product was deleted", result = tobedeleted });
            }
            else
            {
                return Json(new { message = "This product does not exist" });
            }

        }
        [HttpPut, Route("update/{id:int}")]
        public async Task<JsonResult> update([FromRoute(Name = "id")] int id,[FromBody] Product pr)
        {
            Console.WriteLine(id);
            pr.IdPr = id;
            Product tobeupdated =await _productService.getPr(pr.IdPr);
            if (tobeupdated != null)
            {
                await _productService.UpdatePr(pr);
                return Json(new { success = true, message = "the below product was updated", result = pr });
            }
            else
            {
                return Json(new { message = "This product does not exist" });
            }

        }
      
    }
}
