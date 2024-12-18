using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    public class MoneyManagementDAO 
    {
        private readonly GameStore2Context _context = new GameStore2Context();

        public async Task<IEnumerable<MoneyManagement>> GetListAll()
        {
            return await _context.MoneyManagements.ToListAsync();
        }

        public async Task<IEnumerable<(DateTime date,long revenue)>> GetTimeRevenue(string sellerName)
        {
            var result = await (from mm in _context.MoneyManagements
                         join od in _context.OrderDetails on mm.Odid equals od.Odid
                         join p in _context.Profiles on mm.Username equals p.Username
                         where mm.Username == sellerName
                         orderby mm.Date
                         select new
                         {
                             Date = mm.Date,
                             Revenue = mm.SellerMoney
                         }).ToListAsync();
            return result.Select(x => (x.Date, x.Revenue));
        }

        public async Task<IEnumerable<(string ID, DateTime date, string cusName, long gamePrice, long sellerPlusMoney)>> GetGameRevenue(string sellerName, string gameId)
        {
            var result = await (from mm in _context.MoneyManagements
                                join od in _context.OrderDetails on mm.Odid equals od.Odid
                                join o in _context.Orders on od.Oid equals o.Oid
                                join g in _context.Games on od.Gid equals g.Gid
                                where mm.Username == sellerName
                                && g.Gid == gameId
                                orderby mm.Date
                                select new
                                {
                                    ID = od.Odid,
                                    date = mm.Date,
                                    cusName = o.Username,
                                    gamePrice = od.Price,
                                    sellerPlusMoney = mm.SellerMoney
                                }).ToListAsync();
            return result.Select(x => (x.ID, x.date, x.cusName, x.gamePrice, x.sellerPlusMoney));
        }

        public async Task<IEnumerable<(string ID, string GID, DateTime date, long sellerMoney, long plusMoney)>> GetWebTotalRevene()
        {
            var result = await (from mm in _context.MoneyManagements
                                join od in _context.OrderDetails on mm.Odid equals od.Odid
                                join g in _context.Games on od.Gid equals g.Gid
                                orderby mm.Date
                                select new
                                {
                                    ID = od.Odid,
                                    GID = od.Gid,
                                    date = mm.Date,
                                    sellerMoney = mm.SellerMoney,
                                    plusMoney = mm.Admoney
                                }).ToListAsync();
            return result.Select(x => (x.ID, x.GID, x.date, x.sellerMoney, x.plusMoney));
        }

        // Get a Feedback by Id
        public async Task<MoneyManagement> GetById(string id, string oid)
        {
            var item = await _context.MoneyManagements.FirstOrDefaultAsync(c => c.Username == id && c.Odid == oid);
            if (item == null)
            {
                return null;
            }
            return item;
        }

        // Add a new Feedback
        public async Task Add(MoneyManagement item)
        {
            _context.MoneyManagements.Add(item);
            await _context.SaveChangesAsync();
        }

        // Update an existing Feedback
        public async Task Update(MoneyManagement item)
        {
            var existingItem = await GetById(item.Username, item.Odid);
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
                _context.MoneyManagements.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
