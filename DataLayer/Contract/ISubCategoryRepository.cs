using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Contract
{
    public interface ISubCategoryRepository
    {
        public void Add(SubCategory subcat);
        public Task<List<SubCategory>> GetSubCategories();
        public void Delete(SubCategory subcat);

        public void Update(SubCategory subcat);
        public bool exists(SubCategory subcat);

        public Task<SubCategory> getSpecific(int id);

        public string getName(int id);

        public int getId(string name);
    }
}
