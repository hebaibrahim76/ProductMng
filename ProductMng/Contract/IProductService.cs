using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;

namespace ProductMng.Contract
{
    public interface IProductService
    {
        public Task<List<Product>> GetAll();
        public Task AddPr(Product pr);

        public Task DeletePr(Product pr);

        public Task UpdatePr(Product pr);

        public Task<Product> getPr(int id);
        public bool exists(Product pr);
    }
}
