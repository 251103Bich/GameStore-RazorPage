using DataAccess.DAOs;
using Models;
using Models.ViewModel;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private ProfileDAO profileDAO;
        private FeedbackDAO feedbackDAO;
        public ProfileRepository(ProfileDAO item, FeedbackDAO item2) {
            profileDAO = item;
            feedbackDAO = item2;
        }
        public async Task<Profile> Login(string username, string password) { 
            return await profileDAO.Login(username, password);
        }
        public async Task<bool> Register(string username, string password, string email, string type)
        {
            return await profileDAO.Register(username, password, email, type);
        }
        public async Task<IEnumerable<Profile>> GetListAll() => await profileDAO.GetListAll();
        public async Task<IEnumerable<Profile>> GetUserByType(string type) => await profileDAO.GetUserByType(type);
        public async Task<Profile> GetById(string id) => await profileDAO.GetById(id);
        public async Task<Profile> GetByEmail(string email) => await profileDAO.GetByEmail(email);

        public async Task Add(Profile item) => await profileDAO.Add(item);
        public async Task Update(Profile item) => await profileDAO.Update(item);
        public async Task Delete(string id) => await profileDAO.Delete(id);
        public async Task UpdateWishlist(Profile item, string item2) => await profileDAO.UpdateWishlist(item, item2);
        public async Task<Boolean> ChangePassword(string username, string password) => await profileDAO.ChangePassword(username, password);

        public async Task<Boolean> ChangeInformation(string username, string fullname, string gender, DateTime? birthDate) => await profileDAO.ChangeInformation(username, fullname, gender, birthDate);    

        public async Task<IEnumerable<Game>> GetUserLibrary(Profile item) => await profileDAO.GetUserLibrary(item);
        public async Task AddFeedback(Feedback item, string item2) => await feedbackDAO.Add(item, item2);

        public async Task<Boolean> CheckOTP(string email, string otp, string type) => await profileDAO.CheckOTP(email, otp, type);
        public async Task<Boolean> ResendOTP(string email) => await profileDAO.ResendOTP(email);
        public async Task<IEnumerable<Purchase>> GetCountLibrary(Profile profile) => await profileDAO.GetCountLibrary(profile);

        public async Task<bool> RegisterGoogle(string username, string password, string email, string type) => await profileDAO.RegisterGoogle(username, password, email, type);
        public async Task<Boolean> ConfirmGoogle(string email) => await profileDAO.ConfirmGoogle(email);

        public async Task<bool> GetCheckLibrary(string username, string Gid) => await profileDAO.GetCheckLibrary(username, Gid);
        public async Task<Profile> LoginAdmin(string username, string password) => await profileDAO.LoginAdmin(username, password);
        public async Task<Profile> ApproveSeller(string username) => await profileDAO.ApproveSeller(username);

        public async Task<IEnumerable<Profile>> GetSellerAll() => await profileDAO.GetSellerAll();
        public async Task<IEnumerable<Profile>> GetSellerApproveAll() => await profileDAO.GetSellerApproveAll();

        public async Task<Profile> GetByIdCard(string id) => await profileDAO.GetByIdCard(id);
        public async Task DeleteCard(string gid, string uid) => await profileDAO.DeleteCard(gid, uid);

        public async Task<IEnumerable<Profile>> GetDeleteSeller() => await profileDAO.GetDeleteSeller();

        public async Task AddSeller(Profile item) => await profileDAO.AddSeller(item);
    }
}
