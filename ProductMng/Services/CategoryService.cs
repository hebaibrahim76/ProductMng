using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Contract;
using DataLayer.Models;
using ProductMng.Contract;

namespace ProductMng.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _catRepository;
        public CategoryService(ICategoryRepository catRepository)
        {
            _catRepository = catRepository;
        }
        public async Task<List<Category>> GetAll()
        {
            return await _catRepository.GetCategories();
        }
        public void AddCat(Category cat)
        {
            _catRepository.Add(cat);
        }
        public void DeleteCat(Category cat)
        {
            if (cat != null)
                _catRepository.Delete(cat);
        }
        public void UpdateCat(Category cat)
        {
            if (cat != null)
                _catRepository.Update(cat);
        }
        public async Task<Category> getCat(int id)
        {
            return await _catRepository.getSpecific(id);
        }
        public bool exists(Category cat)
        {
            return _catRepository.exists(cat);
        }
    }
}
