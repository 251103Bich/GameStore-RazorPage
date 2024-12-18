using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    public class MoneyHistoryDAO 
    {
        private readonly GameStore2Context _context = new GameStore2Context();

        public async Task<IEnumerable<MoneyHistory>> GetListAll()
        {
            return await _context.MoneyHistories.ToListAsync();
        }

        public async Task<IEnumerable<MoneyHistory>> GetByUsername(string user)
        {
            return await _context.MoneyHistories
                .Where(c =>  c.Username == user)
                .ToListAsync();
        }

        // Get a MoneyHistory by Id
        public async Task<MoneyHistory> GetById(string id)
        {
            var item = await _context.MoneyHistories.FirstOrDefaultAsync(c => c.Mid == id);
            if (item == null)
            {
                return null;
            }
            return item;
        }

        // Add a new MoneyHistory
        public async Task Add(MoneyHistory item)
        {
            _context.MoneyHistories.Add(item);
            await _context.SaveChangesAsync();
        }

        // Update an existing MoneyHistory
        public async Task Update(MoneyHistory item)
        {
            var existingItem = await GetById(item.Mid);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
        }

        // Delete a MoneyHistory by Id
        public async Task Delete(string id)
        {
            var item = await GetById(id);
            if (item != null)
            {
                _context.MoneyHistories.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
