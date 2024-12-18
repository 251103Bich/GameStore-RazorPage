using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    public class CategoryDAO 
    {
        private readonly GameStore2Context _context = new GameStore2Context();

        public async Task<IEnumerable<Category>> GetListAll()
        {
            return await _context.Categories.ToListAsync();
        }

        //Get Categories by Gid
        public async Task<IEnumerable<Category>> GetCategoriesByGameId(string id)
        {
            var categoriesList = await (from c in _context.Categories
                                        where c.Gids.Any(g => g.Gid == id)
                                        select c).ToListAsync();

            return categoriesList;
        }

        // Get a Category by Id
        public async Task<Category> GetById(string id)
        {
            var item = await _context.Categories.FirstOrDefaultAsync(c => c.Cid == id);
            if (item == null)
            {
                return null;
            }
            return item;
        }

        // Add a new Category
        public async Task Add(Category item)
        {
            _context.Categories.Add(item);
            await _context.SaveChangesAsync();
        }

        // Update an existing Category
        public async Task Update(Category item)
        {
            var existingItem = await GetById(item.Cid);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
        }

        // Delete a Category by Id
        public async Task Delete(string id)
        {
            var item = await GetById(id);
            if (item != null)
            {
                _context.Categories.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
