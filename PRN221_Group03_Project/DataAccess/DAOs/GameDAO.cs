using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess.DAOs
{
    public class GameDAO 
    {
        private readonly GameStore2Context _context = new GameStore2Context();

        public async Task<IEnumerable<Game>> GetListAll()
        {
            return await _context.Games.Where(c => c.Status == "Enable").ToListAsync();
        }

        public async Task<IEnumerable<Game>> GetTopSaleGameList()
        {
            var topSaleGame = await (from g in _context.Games
                                         join od in _context.OrderDetails on g.Gid equals od.Gid
                                         where g.Status == "Enable"
                                         group g by g into gameGroup
                                         orderby gameGroup.Count() descending
                                         select gameGroup.FirstOrDefault()).Take(8).ToListAsync();

            return topSaleGame;
        }

        public async Task<IEnumerable<Game>> GetNewGameList()
        {
            var newGame = await (from g in _context.Games
                                 where g.Status == "Enable"
                                 orderby g.Date descending
                                 select g).Take(8).ToListAsync();

            return newGame;
        }

        public async Task<IEnumerable<Game>> GetGamesBySellerName(string sellerName)
        {
            var sellerGame = await (from g in _context.Games
                                    where g.Status == "Enable" && g.SellerName == sellerName
                                    orderby g.Date descending
                                    select g).ToListAsync();
            return sellerGame;
        }

        // Get a Game by Id
        public async Task<Game> GetById(string id)
        {
            var item = await _context.Games.Include(g => g.Cids).FirstOrDefaultAsync(c => c.Gid == id);
            if (item == null)
            {
                return null;
            }
            return item;
        }

        public async Task<List<Category>> GetCategoryByGameId(string item)
        {
            var result = await _context.Games
                        .Where(g => g.Gid == item)
                        .SelectMany(g => g.Cids)
                        .ToListAsync();
            if (item == null)
            {
                return null;
            }
            return result;
        }

        public async Task<IEnumerable<Game>> GetGamesByGameName(string name)
        {
            var gameList = await _context.Games
                .Where(g => g.Status == "Enable" && g.GameName.ToLower().Contains(name.ToLower()))
                .ToListAsync();
            return gameList;
        }

        // Add a new Game
        public async Task Add(Game item, List<string> item2)
        {
            item.Gid = string.Concat(item.GameName.Split(' ').Select(word => word[0])).ToUpper() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            item.Status = "Waiting";
            item.Date = DateTime.Now;
            foreach (var c in item2)
            {
                var category = _context.Categories.Find(c);
                if (category != null) item.Cids.Add(category);
            }
            _context.Games.Add(item);
            await _context.SaveChangesAsync();
        }

        // Update an existing Game
        public async Task Update(Game item, List<string> item2)
        {
            var existingItem = await GetById(item.Gid);
            item.Date = existingItem.Date;
            item.Status = existingItem.Status;
            item.SellerName = existingItem.SellerName;
            item.UpdateDate = DateTime.Now;
            if (existingItem != null)
            {
                existingItem.Cids.Clear();
				await _context.SaveChangesAsync();
				_context.Entry(existingItem).CurrentValues.SetValues(item);
                foreach (var c in item2)
                {
                    var category = _context.Categories.Find(c);
                    if (category != null) existingItem.Cids.Add(category);
                }
            }
            await _context.SaveChangesAsync();
        }

        // Delete a Game by Id
        public async Task Delete(string id)
        {
            var item = await GetById(id);
            if (item != null)
            {
                if (item.Status == "Disable") item.Status = "Enable";
                else item.Status = "Disable";
                _context.Entry(item).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Game>> GetGameDelete()
        {
            var item = await _context.Games.Where(g => g.Status == "Disable").ToListAsync();
            return item;
        }

        public async Task<IEnumerable<Game>> searchGameByCategory(string id)
		{
            var listGame = await (from g in _context.Games
                                        where g.Cids.Any(g => g.Cid == id)
                                        select g).ToListAsync();
            return listGame;
		}

        public async Task AddCard(string gid, string uid)
        {
            var user = await _context.Profiles.FindAsync(uid);
            var game = await _context.Games.FindAsync(gid);

            if (user != null && game != null)
            {
                game.Usernames.Add(user);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> GetCard(string uid)
        {
            var user = await _context.Profiles
                                     .Include(p => p.Gids)
                                     .FirstOrDefaultAsync(p => p.Username == uid);

            if (user == null || !user.Gids.Any())
            {
                return Enumerable.Empty<Game>();
            }

            return user.Gids;
        }
        public async Task DeleteCard(string gid, string uid)
        {
            var user = await _context.Profiles.FindAsync(uid);
            var game = await _context.Games.FindAsync(gid);

            if (user != null && game != null)
            {
                game.Usernames.Remove(user);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> GetMyGames(Profile item)
        {
            return await _context.Games.Where(u => u.SellerName == item.Username && u.Status != "AbsolutelyInactive").ToListAsync();
        }

        public async Task<bool> CheckCart(string uid, string gid)
        {
            var check = await _context.Profiles
                .Include(p => p.Gids)
                .AnyAsync(p => p.Username == uid && p.Gids.Any(g => g.Gid == gid));

            return check;
        }
        public async Task<Game> ApproveGame(string username)
        {
            var item = await _context.Games.FirstOrDefaultAsync(i => i.Gid == username);
            if (item == null)
            {
                return null;
            }
            item.Status = "Enable";
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<Game>> GetGameApproveAll()
        {
            return await _context.Games.Where(t => t.Status == "Waiting").ToListAsync();
        }
    }
}
