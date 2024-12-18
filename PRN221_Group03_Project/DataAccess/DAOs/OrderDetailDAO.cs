using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    public class OrderDetailDAO 
    {
        private readonly GameStore2Context _context = new GameStore2Context();

        public async Task<IEnumerable<OrderDetail>> GetListAll()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        // Get a OrderDetail by Id
        public async Task<OrderDetail> GetById(string id)
        {
            var item = await _context.OrderDetails.FirstOrDefaultAsync(c => c.Odid == id);
            if (item == null)
            {
                return null;
            }
            return item;
        }

        // Add a new OrderDetail
        public async Task Add(OrderDetail item)
        {
            _context.OrderDetails.Add(item);
            await _context.SaveChangesAsync();
        }

        // Update an existing OrderDetail
        public async Task Update(OrderDetail item)
        {
            var existingItem = await GetById(item.Odid);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
        }

        // Delete a OrderDetail by Id
        public async Task Delete(string id)
        {
            var item = await GetById(id);
            if (item != null)
            {
                _context.OrderDetails.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
