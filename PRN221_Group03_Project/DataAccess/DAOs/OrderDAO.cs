using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    public class OrderDAO 
    {
        private readonly GameStore2Context _context = new GameStore2Context();

        public async Task<IEnumerable<Order>> GetListAll()
        {
            return await _context.Orders.ToListAsync();
        }

        // Get a Order by Id
        public async Task<Order> GetById(string id)
        {
            var item = await _context.Orders.FirstOrDefaultAsync(c => c.Oid == id);
            if (item == null)
            {
                return null;
            }
            return item;
        }

        // Add a new Order
        public async Task Add(Order item)
        {
            _context.Orders.Add(item);
            await _context.SaveChangesAsync();
        }

        // Update an existing Order
        public async Task Update(Order item)
        {
            var existingItem = await GetById(item.Oid);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
        }

        // Delete a Order by Id
        public async Task Delete(string id)
        {
            var item = await GetById(id);
            if (item != null)
            {
                _context.Orders.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
