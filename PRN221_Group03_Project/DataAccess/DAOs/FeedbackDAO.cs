using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    public class FeedbackDAO
    {
        private readonly GameStore2Context _context = new GameStore2Context();

        public async Task<IEnumerable<Feedback>> GetListAll()
        {
            return await _context.Feedbacks.ToListAsync();
        }

        // Get a Feedback by Id
        public async Task<Feedback> GetById(string id, string gid)
        {
            var item = await _context.Feedbacks.FirstOrDefaultAsync(c => c.Gid == gid && c.Username == id);
            if (item == null)
            {
                return null;
            }
            return item;
        }

        // Add a new Feedback
        public async Task Add(Feedback item, string item2)
        {
            item.Username = item2;
            item.Date = DateTime.Now;
            item.Status = "Active";
            _context.Feedbacks.Add(item);
            await _context.SaveChangesAsync();
        }

        // Update an existing Feedback
        public async Task Update(Feedback item)
        {
            var existingItem = await GetById(item.Username, item.Gid);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
        }

        // Delete a Feedback by Id
        public async Task Delete(string id, string gid)
        {
            var item = await GetById(id, gid);
            if (item != null)
            {
                _context.Feedbacks.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbackByGID(string gid)
        {
            var item = await _context.Feedbacks.Where(g => g.Gid == gid).OrderByDescending(d => d.Date).ToListAsync();
            if (item == null)
            {
                return null;
            }
            return item;
        }
    }
}
