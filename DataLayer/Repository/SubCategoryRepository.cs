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
    public class SubCategoryRepository:ISubCategoryRepository
    {
        productManagementContext _context;
        public SubCategoryRepository(productManagementContext context)
        {
            _context = context;
        }
        public async Task<List<SubCategory>> GetSubCategories()
        {
            return await _context.SubCategories.ToListAsync();
        }
        public void Add(SubCategory subcat)
        {
            _context.SubCategories.Add(subcat);
            _context.SaveChanges();
        }
        public void Delete(SubCategory subcat)
        {
            if (subcat != null)
            {
                _context.SubCategories.Remove(subcat);
                _context.SaveChanges();
            }
        }
        public void Update(SubCategory subcat)
        {
            if (subcat != null)
            {
                _context.SubCategories.Update(subcat);
                _context.SaveChanges();
            }

        }
        public bool exists(SubCategory subcat)
        {
            List<SubCategory> list = _context.SubCategories.Where(e => e == subcat).ToList<SubCategory>();
            if (!list.Any())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public async Task<SubCategory> getSpecific(int id)
        {
            List<SubCategory> list = _context.SubCategories.AsNoTracking().Where(e => e.IdSub == id).ToList<SubCategory>();
            if (!list.Any())
            {
                return null;
            }
            else
            {
                return _context.SubCategories.AsNoTracking().Where(e => e.IdSub == id).ToList<SubCategory>().First<SubCategory>();
            }
        }
        public string getName(int id)
        {
            return _context.SubCategories.Find(id).Name;
        }
        public int getId(string name)
        {
            return _context.SubCategories.Where(e => e.Name == name).First().IdSub;
        }
    }
}
