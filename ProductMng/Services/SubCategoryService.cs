using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Contract;
using DataLayer.Models;
using ProductMng.Contract;


namespace ProductMng.Services
{
    public class SubCategoryService:ISubCategoryService
    {

        private readonly ISubCategoryRepository _subcatRepository;
        public SubCategoryService(ISubCategoryRepository subcatRepository)
        {
            _subcatRepository = subcatRepository;
        }
        public async Task<List<SubCategory>> GetAll()
        {
            return await _subcatRepository.GetSubCategories();
        }
        public void AddSubCat(SubCategory subcat)
        {
            _subcatRepository.Add(subcat);
        }
        public void DeleteSubCat(SubCategory subcat)
        {
            if (subcat != null)
                _subcatRepository.Delete(subcat);
        }
        public void UpdateSubCat(SubCategory subcat)
        {
            if (subcat != null)
                _subcatRepository.Update(subcat);
        }
        public async Task<SubCategory> getSubCat(int id)
        {
            return await _subcatRepository.getSpecific(id);
        }
        public bool exists(SubCategory subcat)
        {
            return _subcatRepository.exists(subcat);
        }
        public string getName(int id)
        {
            return _subcatRepository.getName(id);
        }
        public int getId(string name)
        {
            return _subcatRepository.getId(name);
        }
    }
}
