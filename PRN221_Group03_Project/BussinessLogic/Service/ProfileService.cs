using BussinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Models;
using Models.ViewModel;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Service
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository profileRepository;
        public ProfileService(IProfileRepository item)
        {
            profileRepository = item;
        }
        public async Task<Profile> Login(string username, string password) {
            return await profileRepository.Login(username, password);
        }

        public async Task<bool> Register(string username, string password, string email, string type)
        {
            return await profileRepository.Register(username, password, email, type);
        }
        public async Task<IEnumerable<Profile>> GetListAllProfile()
        {
            return await profileRepository.GetListAll();
        }

        public async Task<IEnumerable<Profile>> GetUserByType(string type)
        {
            return await profileRepository.GetUserByType(type);
        }

        public async Task<Profile> GetProfileByUsername(string id)
        {
            return await profileRepository.GetById(id);
        }
        public async Task<Profile> GetByEmail(string email)
        {
            return await profileRepository.GetByEmail(email);
        }

        public async Task AddProfile(Profile item)
        {
            // Thêm logic nghiệp vụ nếu cần
            await profileRepository.Add(item);
        }

        public async Task UpdateProfile(Profile item)
        {
            // Thêm logic nghiệp vụ nếu cần
            await profileRepository.Update(item);
        }

        public async Task DeleteProfile(string id)
        {
            // Thêm logic nghiệp vụ nếu cần
            await profileRepository.Delete(id);
        }
        public async Task UpdateWishlist(Profile item, string item2) => await profileRepository.UpdateWishlist(item, item2);
        public async Task<IEnumerable<Game>> GetUserLibraryAsync(Profile item) => await profileRepository.GetUserLibrary(item);
        public async Task AddFeedbackAsync(Feedback item, string item2) => await profileRepository.AddFeedback(item, item2);

        public async Task<Boolean> ChangePassword(string username, string password) => await profileRepository.ChangePassword(username, password);
        public async Task<Boolean> ChangeInformation(string username, string fullname, string gender, DateTime? birthDate) => await profileRepository.ChangeInformation(username, fullname, gender, birthDate);

        public async Task<Boolean> CheckOTP(string email, string otp, string type) => await profileRepository.CheckOTP(email, otp, type);
        public async Task<Boolean> ResendOTP(string email) => await profileRepository.ResendOTP(email);

        public async Task<IEnumerable<Purchase>> GetCountLibrary(Profile profile) => await profileRepository.GetCountLibrary(profile);

        public async Task<bool> RegisterGoogle(string username, string password, string email, string type) => await profileRepository.RegisterGoogle(username, password, email, type);
        public async Task<Boolean> ConfirmGoogle(string email) => await profileRepository.ConfirmGoogle(email);
    
        public async Task<bool> GetCheckLibrary(string username, string Gid)
        {
            return await profileRepository.GetCheckLibrary(username, Gid);
        }

        public async Task<Profile> LoginAdmin(string username, string password)
       => await profileRepository.LoginAdmin(username, password);

        public async Task<Profile> ApproveSeller(string username)
        {
            return await profileRepository.ApproveSeller(username);
        }

        public async Task<IEnumerable<Profile>> GetSellerAll()
        {
            return await profileRepository.GetSellerAll();
        }
        public async Task<IEnumerable<Profile>> GetSellerApproveAll()
        {
            return await profileRepository.GetSellerApproveAll();
        }

        public async Task<Profile> LoginAdminn(string username, string password)
         => await profileRepository.LoginAdmin(username, password);
        public async Task<Profile> GetByIdCard(string id) => await profileRepository.GetByIdCard(id);
        public async Task DeleteCard(string gid, string uid) => await profileRepository.DeleteCard(gid, uid);
        public async Task AddSeller(Profile item)
        {
            await profileRepository.AddSeller(item);
        }

        public async Task<IEnumerable<Profile>> GetDeleteSeller()
        {
            return await profileRepository.GetDeleteSeller();
        }
    }
}
