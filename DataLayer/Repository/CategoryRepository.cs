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
    public class CategoryRepository:ICategoryRepository
    {

        productManagementContext _context;
        public CategoryRepository(productManagementContext context)
        {
            _context = context;
        }
        public async  Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public void Add(Category cat)
        {
            _context.Categories.Add(cat);
            _context.SaveChanges();
        }
        public void Delete(Category cat)
        {
            if (cat != null)
            {
                _context.Categories.Remove(cat);
                _context.SaveChanges();
            }
        }
        public void Update(Category cat)
        {
            if (cat != null)
            {
                _context.Categories.Update(cat);
                _context.SaveChanges();
            }

        }
        public bool exists(Category cat)
        {
            List<Category> list = _context.Categories.Where(e => e == cat).ToList<Category>();
            if (!list.Any())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public async Task<Category> getSpecific(int id)
        {
            List<Category> list = _context.Categories.AsNoTracking().Where(e => e.IdCategory == id).ToList<Category>();
            if (!list.Any())
            {
                return null;
            }
            else
            {
                return _context.Categories.AsNoTracking().Where(e => e.IdCategory == id).ToList<Category>().First<Category>();
            }
        }
        

    }
}
