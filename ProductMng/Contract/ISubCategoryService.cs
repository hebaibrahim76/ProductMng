using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;

namespace ProductMng.Contract
{
    public interface ISubCategoryService
    {
        public Task<List<SubCategory>> GetAll();
        public void AddSubCat(SubCategory subcat);

        public void DeleteSubCat(SubCategory subcat);

        public void UpdateSubCat(SubCategory subcat);

        public Task<SubCategory> getSubCat(int id);
        public bool exists(SubCategory subcat);
        public string getName(int id);
        public int getId(string name);
    }
}
