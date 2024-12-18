using Models;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Interfaces
{
    public interface IProfileService
    {
        Task<Profile> Login(string username, string password);
        Task<bool> Register(string username, string password, string email, string type);
        Task<IEnumerable<Profile>> GetListAllProfile();
        Task<IEnumerable<Profile>> GetUserByType(string type);
        Task<Profile> GetProfileByUsername(string id);
        Task<Profile> GetByEmail(string email);

        Task AddProfile(Profile item);
        Task UpdateProfile(Profile item);
        Task DeleteProfile(string id);
        Task UpdateWishlist(Profile item, string item2);
        Task<Boolean> ChangePassword(string username,string password);
        Task<Boolean> ChangeInformation(string username, string fullname, string gender, DateTime? birthDate);
        Task<IEnumerable<Game>> GetUserLibraryAsync(Profile item);
        Task AddFeedbackAsync(Feedback item, string item2);
        Task<Boolean> CheckOTP(string email, string otp, string type);
        Task<Boolean> ResendOTP(string email);
        Task<IEnumerable<Purchase>> GetCountLibrary(Profile profile);
        Task<bool> RegisterGoogle(string username, string password, string email, string type);
        Task<Boolean> ConfirmGoogle(string email);

        Task<bool> GetCheckLibrary(string username, string Gid);
        Task<Profile> ApproveSeller(string username);

        Task<IEnumerable<Profile>> GetSellerAll();
        Task<IEnumerable<Profile>> GetSellerApproveAll();

        Task<Profile> LoginAdminn(string username, string password);
        Task<Profile> GetByIdCard(string id);
        Task DeleteCard(string gid, string uid);
        Task AddSeller(Profile item);
        Task<IEnumerable<Profile>> GetDeleteSeller();

    }
}
