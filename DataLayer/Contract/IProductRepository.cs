using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using System.Linq;


namespace DataLayer.Contract
{
    public interface IProductRepository
    {
        public Task Add(Product pr);
        public Task<List<Product>> GetProducts();
        public Task Delete(Product pr);

        public Task Update(Product pr);
        public bool exists(Product pr);

        public Task<Product> getSpecific(int id);
    }
}
