using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Identity.Client;
using Models;
using Models.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    public class ProfileDAO 
    {
        private readonly GameStore2Context _context = new GameStore2Context();

        public async Task<IEnumerable<Profile>> GetListAll()
        {
            return await _context.Profiles.Include(g => g.GidsNavigation).ToListAsync();
        }

        // Get a Profile by Id
        public async Task<Profile> GetById(string id)
        {
            var item = await _context.Profiles.Include(g => g.GidsNavigation).ThenInclude(c => c.Cids).FirstOrDefaultAsync(c => c.Username == id);
            if (item == null)
            {
                return null;
            }
            return item;
        }
        public async Task<Profile> GetByIdCard(string id)
        {
            var item = await _context.Profiles.Include(g => g.Gids).ThenInclude(c => c.Cids).FirstOrDefaultAsync(c => c.Username == id);
            if (item == null)
            {
                return null;
            }
            return item;
        }
        public async Task<IEnumerable<Profile>> GetUserByType(string type)
        {
            var item = await _context.Profiles.
                Where(p => p.Type == type)
                .ToListAsync();
            return item;
        }

        public async Task<IEnumerable<Profile>> GetSellerAll()
        {
            var item = await _context.Profiles.Where(s => s.Type == "Seller" && s.Status == "Enable").ToListAsync();
            return item;
        }

        public async Task<Profile> GetByEmail(string email)
        {
            var item = await _context.Profiles.FirstOrDefaultAsync(m => m.Email == email);
            if (item == null)
            {
                return null;
            }
            return item;
        }
        // Add a new Profile
        public async Task Add(Profile item)
        {
            _context.Profiles.Add(item);
            await _context.SaveChangesAsync();
        }

        // Update an existing Profile
        public async Task Update(Profile item)
        {
            var existingItem = await GetById(item.Username);
            if (existingItem != null)
            {
                existingItem.Password = item.Password;
                _context.Entry(existingItem).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
        }

        // Delete a Profile by Id
        public async Task Delete(string id)
        {
            var item = await GetById(id);
            if (item != null)
            {
                if (item.Status == "Disable") item.Status = "Enable";
                else item.Status = "Disable";
                _context.Update(item);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> Register(string username, string password, string email, string type)
        {
            try {
                var item = await _context.Profiles.FirstOrDefaultAsync(c => c.Username == username || c.Email == email);
                if (item != null)
                {
                    return false;
                }

                var temp = new Profile();
                string randomString = "";
                do
                {
                    RandomCode randomStringGenerator = new RandomCode();
                    randomString = randomStringGenerator.GenerateRandomString(6);
                    temp = await _context.Profiles.FirstOrDefaultAsync(c => c.Token == randomString);
                }
                while (temp != null);
                Profile profile = new Profile();
                profile.Username = username.Trim();
                profile.Password = GetMd5(password.Trim());
                profile.Email = email.Trim();
                profile.Money = 0;
                profile.Type = type;
                profile.Status = "Confirm";
                profile.Date = DateTime.Now;
                profile.Token = randomString;
                _context.Profiles.Add(profile);
                _context.SaveChanges();
                return true;

            }
            catch {
                return false;
            }

        }

        public async Task<bool> RegisterGoogle(string username, string password, string email, string type)
        {
            try
            {
                string tempName = username.Split('@')[0];
                RandomCode randomStringGenerator = new RandomCode();
                string userString = "";
                var item = await _context.Profiles.FirstOrDefaultAsync(c => c.Username == tempName);
                if (item != null)
                {
                    do
                    {
                        userString = randomStringGenerator.GenerateRandomString(6);
                        tempName = "Guest" + userString;
                        item = await _context.Profiles.FirstOrDefaultAsync(c => c.Username == tempName);
                    }while (item != null);
                }
                
                string randomString = "";
                
                randomString = randomStringGenerator.GenerateRandomString(20);                
               
                Profile profile = new Profile();
                profile.Username = tempName.Trim();
                profile.Password = GetMd5(randomString.Trim());
                profile.Email = email.Trim();
                profile.Money = 0;
                profile.Type = type;
                profile.Status = "Enable";
                profile.Date = DateTime.Now;
                profile.Token = null;
                _context.Profiles.Add(profile);
                _context.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }

        }
        public async Task<Profile> Login(string username, string password) {
            var item = await _context.Profiles.FirstOrDefaultAsync(c => c.Username == username && c.Password == GetMd5(password));
            return item;
        }

        public Task<Profile> LoginAdmin(string username, string password)
        {
            var item = _context.Profiles.FirstOrDefaultAsync(c => c.Username == username && c.Password == GetMd5(password) && c.Type == "Admin");
            return item;
        }

        public static string GetMd5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to a hexadecimal string.
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public async Task UpdateWishlist(Profile item, string item2)
        {
            var check = item.GidsNavigation;
            var game = await _context.Games.FirstOrDefaultAsync(g => g.Gid == item2);
            if (item != null && item2 != null && !check.Contains(game))
                item.GidsNavigation.Add(game);
            else
                item.GidsNavigation.Remove(game);
            await _context.SaveChangesAsync();
        }

        public async Task<Boolean> ChangePassword(string username, string password) {
            try {
                var temp = await _context.Profiles.FirstOrDefaultAsync(f => f.Username == username);
                temp.Password = GetMd5(password);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        public async Task<Boolean> ChangeInformation(string username, string fullname, string gender, DateTime? birthDate) {
            try
            {
                var temp = await _context.Profiles.FirstOrDefaultAsync(f => f.Username == username);
                temp.Fullname = fullname;
                temp.Gender = gender;
                temp.Birthday = birthDate;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        public async Task<IEnumerable<Game>> GetUserLibrary(Profile item)
        {
            var result = await _context.Orders
                .Include(o => o.OrderDetails)
                .Where(o => o.Username == item.Username).SelectMany(o => o.OrderDetails.Select(od => od.Gid))
                .Distinct().ToListAsync();
            var games = await _context.Games
                .Where(g => result.Contains(g.Gid)).Include(c => c.Cids)
				.ToListAsync();
            return games;
        }

        public async Task<Boolean> CheckOTP(string email, string otp, string type)
        {
            var temp = await _context.Profiles.FirstOrDefaultAsync(f => f.Email == email && f.Token == otp);
            if (temp == null) return false;
            if(type == "Register")
            {
                if (temp.Type == "Seller" && temp.Status == "Confirm")
                {
                    temp.Status = "Waiting";
                    temp.Token = null;
                    _context.SaveChanges();
                }
                if (temp.Type == "Customer" && temp.Status == "Confirm")
                {
                    temp.Status = "Enable";
                    temp.Token = null;
                    _context.SaveChanges();
                }
            }
            if(type == "ResetPass")
            {
                temp.Token = null;
                _context.SaveChanges();
            }
            
            return true;
        }

        public async Task<Boolean> ResendOTP(string email)
        {
            var temp = await _context.Profiles.FirstOrDefaultAsync(f => f.Email == email);
            if (temp == null)
            {
                return false ;
            }
            string code = null;
            var check = new Profile();
            RandomCode randomCode = new RandomCode();
            do { code = randomCode.GenerateRandomString(6);
                check = await _context.Profiles.FirstOrDefaultAsync(f => f.Token == code);
            }
            while (check != null);
            temp.Token = code;
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<Purchase>> GetCountLibrary(Profile profile)
        {
            /*var resultTemp = await _context.Orders
                .Include(o => o.OrderDetails)
                .Where(o => o.Username == profile.Username)
                .SelectMany(o => o.OrderDetails).ToListAsync();*/

            var result = await (from o in _context.Orders
                                join od in _context.OrderDetails on o.Oid equals od.Oid
                                join g in _context.Games on od.Gid equals g.Gid
                                where o.Username == profile.Username
                                orderby o.Date descending
                                select new Purchase
                                {
                                    OID = o.Oid,
                                    GameName = g.GameName,
                                    Price = g.Price,
                                    Date = o.Date
                                }).ToListAsync();
            
            return result;
        }

        public async Task<Boolean> ConfirmGoogle(string email)
        {
            try
            {
                var temp = await _context.Profiles.FirstOrDefaultAsync(c => c.Email == email);
                if (temp.Type == "Customer")
                {
                    temp.Status = "Enable";
                }
                if (temp.Type == "Seller")
                {
                    temp.Status = "Waiting";
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex) { 
                return false;
            }
        }

        public async Task<bool> GetCheckLibrary(string username, string Gid)
        {

            var exists = await (from o in _context.Orders
                                join od in _context.OrderDetails on o.Oid equals od.Oid
                                join g in _context.Games on od.Gid equals g.Gid
                                where o.Username == username && g.Gid == Gid
                                select o).AnyAsync();

            return exists;
        }

        public async Task<Profile> ApproveSeller(string username)
        {
            var item = await _context.Profiles.FirstOrDefaultAsync(i => i.Username == username);
            if (item == null)
            {
                return null;
            }
            item.Status = "Enable";
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<Profile>> GetSellerApproveAll()
        {
            return await _context.Profiles.Where(t => t.Status == "Waiting").ToListAsync();
        }
        public async Task DeleteCard(string gid, string uid)
        {
            var user = await GetByIdCard(uid);
            var game = await _context.Games.FindAsync(gid);

            if (user != null && game != null)
            {
                user.Gids.Remove(game);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Profile>> GetDeleteSeller()
        {
            var item = await _context.Profiles.Where(g => g.Status == "Disable").ToListAsync();
            return item;
        }

        public async Task AddSeller(Profile item)
        {
            item.Type = "Seller";  // Set the type to "Seller"
            item.Status = "Enable";
            item.Money = 0;
            item.Date = DateTime.Now;
            _context.Profiles.Add(item);
            await _context.SaveChangesAsync();
        }
    }
}
