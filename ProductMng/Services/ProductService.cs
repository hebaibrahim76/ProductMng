using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Contract;
using DataLayer.Models;
using ProductMng.Contract;

namespace ProductMng.Services
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<Product>> GetAll()
        {
            return await _productRepository.GetProducts();
        }
        public async Task AddPr(Product pr)
        {
            await _productRepository.Add(pr);
        }
        public async Task DeletePr(Product pr)
        {
            if (pr != null)
                await _productRepository.Delete(pr);
        }
        public async Task UpdatePr(Product pr)
        {
            if (pr != null)
                await _productRepository.Update(pr);
        }
        public async Task<Product> getPr(int id)
        {
            return await _productRepository.getSpecific(id);
        }
        public bool exists(Product pr)
        {
            return _productRepository.exists(pr);
        }
    }
}
