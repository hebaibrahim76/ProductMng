using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Contract
{
    public interface ICategoryRepository
    {
        public void Add(Category cat);
        public Task<List<Category>> GetCategories();
        public void Delete(Category cat);

        public void Update(Category cat);
        public bool exists(Category cat);

        public Task<Category> getSpecific(int id);
    }
}
