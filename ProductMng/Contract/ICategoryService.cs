using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;

namespace ProductMng.Contract
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetAll();
        public void AddCat(Category cat);

        public void DeleteCat(Category cat);

        public void UpdateCat(Category cat);

        public Task<Category> getCat(int id);
        public bool exists(Category cat);
    }
}
