using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using X.PagedList;

namespace mvc.Controllers
{
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        HttpClient client = new HttpClient();
        string path = "https://localhost:44310/";
        static int delId;
        static int editId;
        // GET: ProductsController
        [Route("getAll")]
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                searchString = searchString.ToLower();
            }
            
            ViewBag.selected = 1;
            List<Product> prInfo = new List<Product>();
            List<SubCategory> subs = new List<SubCategory>();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(path);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("products/getAll");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {

                    var prResponse = Res.Content.ReadAsStringAsync().Result;
                    prInfo = JsonConvert.DeserializeObject<List<Product>>(prResponse);
                }
                
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
                ViewBag.SubSortParm = sortOrder == "Sub" ? "sub_desc" : "Sub";
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;
                var productss = from s in prInfo
                               select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    productss = productss.Where(s => s.Name.ToLower().Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        productss = productss.OrderByDescending(s => s.Name);
                        break;
                    case "Date":
                        productss = productss.OrderBy(s => s.Expiraydate);
                        break;
                    case "date_desc":
                        productss = productss.OrderByDescending(s => s.Expiraydate);
                        break;
                    case "Sub":
                        productss = productss.OrderBy(s => s.SubCategory.Name);
                        break;
                    case "sub_desc":
                        productss = productss.OrderByDescending(s => s.SubCategory.Name);
                        break;
                    default:
                        productss = productss.OrderBy(s => s.Name);
                        break;
                }
                int pageSize = 3;
                int pageNumber = (page ?? 1);
                return View(productss.ToPagedList(pageNumber, pageSize));


            }
        }

        // GET: ProductsController/Details/5
        [Route("details")]
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }

            Product product = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(path);

                var result = await client.GetAsync($"products/specific/{id}");

                if (result.IsSuccessStatusCode)
                {
                    product = await result.Content.ReadAsAsync<Product>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: ProductsController/Create
        [Route("create")]
        public async Task<ActionResult> Create()
        {
            await getCatNames();
            await getSubNames();
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            Product pr = new Product();
            pr.Name = collection["Name"];
            var subname = collection["SubCategory.Name"];
            int r = await GetId(subname);
            pr.SubCategoryId = r;
            pr.Description = collection["Description"];
            pr.Expiraydate = Convert.ToDateTime(collection["Expiraydate"]);
            Console.WriteLine(pr);
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(path);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync("products/post", pr);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ProductsController/Edit/5
        [HttpGet]
        [Route("edit")]
        public async Task<ActionResult> Edit(int id)
        {
            //Product prr = await getProductById(id);
            //ViewBag.idcat = prr.SubCategory.IdCategory;
            await getCatNames();
            await getSubNames();
            editId = id;
            if (id == null)
            {
                return new BadRequestResult();
            }
            Product product = new Product();
            product.IdPr = id;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(path);

                var result = await client.GetAsync($"products/specific/{id}");


                if (result.IsSuccessStatusCode)
                {
                    product = await result.Content.ReadAsAsync<Product>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [Route("edit")]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            id = editId;

            Product pr = new Product();
            pr.IdPr = id;
            pr.Name = collection["Name"];
            var subname = collection["SubCategory.Name"];
            int r = await GetId(subname);
            pr.SubCategoryId = r;
            pr.Description = collection["Description"];
            pr.Expiraydate = Convert.ToDateTime(collection["Expiraydate"]);
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(path);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PutAsJsonAsync($"products/update/{id}", pr);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }

        }

        // GET: ProductsController/Delete/5
        [HttpGet]
        [Route("delete")]
        public async Task<ActionResult> Delete(int id)
        {
            delId = id;
            if (id == null)
            {
                return new BadRequestResult();
            }
            Product product = new Product();
            product.IdPr = id;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(path);

                var result = await client.GetAsync($"products/specific/{id}");

                if (result.IsSuccessStatusCode)
                {
                    product = await result.Content.ReadAsAsync<Product>();
                    var str = result.Content.ToString();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [Route("delete")]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            id = delId;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(path);
                var response = await client.DeleteAsync($"products/delete/{id}");
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpGet]
        [Route("names")]
        public async Task<List<SubCategory>> getSubNames()
        {

            List<SubCategory> subs = new List<SubCategory>();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(path);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("subcategory/getAll");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {

                    var subResponse = Res.Content.ReadAsStringAsync().Result;
                    subs = JsonConvert.DeserializeObject<List<SubCategory>>(subResponse);
                }
            }
            ViewBag.Subs = subs;
            return subs;
        }

        [HttpGet]
        [Route("catnames")]
        public async Task<List<Category>> getCatNames()
        {

            List<Category> subs = new List<Category>();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(path);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("category/getAll");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {

                    var catResponse = Res.Content.ReadAsStringAsync().Result;
                    subs = JsonConvert.DeserializeObject<List<Category>>(catResponse);
                }
            }
            ViewBag.Cats = subs;
            return subs;
        }
        [Route("getid/{name}")]
        public async Task<int> GetId(string name)
        {
            int r = 0;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(path);

                var result = await client.GetAsync($"subcategory/getid/{name}");

                if (result.IsSuccessStatusCode)
                {
                    r = await result.Content.ReadAsAsync<int>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            return r;
        }
        [HttpGet]
        [Route("getProductById/{id}")]
        public async Task<Product> getProductById(int id)
        {
           
            Product product = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(path);

                var result = await client.GetAsync($"products/specific/{id}");

                if (result.IsSuccessStatusCode)
                {
                    product = await result.Content.ReadAsAsync<Product>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            return product;
        }
        

    }
}
