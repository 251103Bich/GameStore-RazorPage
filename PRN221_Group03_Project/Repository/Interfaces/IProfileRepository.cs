using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Models;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IProfileRepository
    {
        Task<Profile> Login(string username, string password);
        Task<bool> Register(string username, string password, string email, string type);

        Task<IEnumerable<Profile>> GetListAll();
        Task<IEnumerable<Profile>> GetUserByType(string type);
        Task<Profile> GetByIdCard(string id);
        Task<Profile> GetById(string id);
        Task<Profile> GetByEmail(string email);

        Task Add(Profile item);
        Task Update(Profile item);
        Task Delete(string id);
        Task UpdateWishlist(Profile item, string item2);
        Task<Boolean> ChangePassword(string username, string password);
        Task<Boolean> ChangeInformation(string username, string fullname, string gender, DateTime? birthDate);


        Task<IEnumerable<Game>> GetUserLibrary(Profile item);
        Task AddFeedback(Feedback item, string item2);
        Task<Boolean> CheckOTP(string email, string otp, string type);
        Task<Boolean> ResendOTP(string email);
        Task<IEnumerable<Purchase>> GetCountLibrary(Profile profile);

        Task<bool> RegisterGoogle(string username, string password, string email, string type);

        Task<Boolean> ConfirmGoogle(string email);

        Task<bool> GetCheckLibrary(string username, string Gid);

        Task<Profile> LoginAdmin(string username, string password);

        Task<Profile> ApproveSeller(string usename);

        Task<IEnumerable<Profile>> GetSellerAll();
        Task<IEnumerable<Profile>> GetSellerApproveAll();
        Task DeleteCard(string gid, string uid);

        Task AddSeller(Profile item);
        Task<IEnumerable<Profile>> GetDeleteSeller();
    }
}
