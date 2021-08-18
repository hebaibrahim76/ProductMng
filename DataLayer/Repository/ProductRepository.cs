using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DataLayer.Models;
using DataLayer.Contract;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class ProductRepository:IProductRepository
    {

        productManagementContext _context;
        public ProductRepository(productManagementContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetProducts()
        {
            var result=await _context.Products.Include(i => i.SubCategory).ToListAsync();
            return result;
        }
        public async Task Add(Product pr)
        {
            _context.Products.Add(pr);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Product pr)
        {
            if (pr != null)
            {
                _context.Products.Remove(pr);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Update(Product pr)
        {
            if (pr != null)
            {
                _context.Products.Update(pr);
                await _context.SaveChangesAsync();
            }

        }
        public bool exists(Product pr)
        {
            List<Product> list = _context.Products.Where(e => e == pr).ToList<Product>();
            if (!list.Any())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public async Task<Product> getSpecific(int id)
        {
            List<Product> list = _context.Products.Include(i => i.SubCategory).AsNoTracking().Where(e => e.IdPr == id).ToList<Product>();
            if (!list.Any())
            {
                return null;
            }
            else
            {
                return _context.Products.Include(i => i.SubCategory).AsNoTracking().Where(e => e.IdPr == id).ToList<Product>().First<Product>();
            }
        }

    }
}
