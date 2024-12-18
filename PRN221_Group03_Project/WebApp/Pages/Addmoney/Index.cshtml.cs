using BussinessLogic.Interfaces;
using BussinessLogic.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace WebApp.Pages.Addmoney
{
    public class IndexModel : PageModel
    {
        private readonly IProfileService _profileService;
        private readonly IMoneyHistoryService _moneyHistoryService;

        [BindProperty]
        public Models.Profile Profile { get; set; } = default!;
        public string FormattedDateTime { get; set; }
        
        public MoneyHistory m = new MoneyHistory();
        public IndexModel(IProfileService profileService, IMoneyHistoryService moneyHistoryService)
        {
            _profileService = profileService;
            _moneyHistoryService = moneyHistoryService;
        }
        public async Task OnGetAsync()
        {
        string item = Request.Cookies["Username"];
        FormattedDateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            Profile = await _profileService.GetProfileByUsername(item);
        }

        public async Task<IActionResult> OnPostAsync(decimal txtmoney)
        {
            // Lấy thông tin Profile
            string item = Request.Cookies["Username"];
            Profile = await _profileService.GetProfileByUsername(item);
            long newMoney = (long)txtmoney;

            DateTime now = DateTime.Now;
            string formattedDateTime = now.ToString("dd/MM/yyyy HH:mm:ss");
            string mid = Profile.Username + formattedDateTime;
            HttpContext.Session.SetString("Addmoney", mid);

            m.Mid = mid;
            m.Username = Profile.Username;
            m.Date = DateTime.Now;
            m.Money = newMoney;
            m.Status = "waiting";
            await _moneyHistoryService.Add(m);
            // Giao dịch thành công, cập nhật trạng thái
            var mHistory = await _moneyHistoryService.GetById(mid);
            mHistory.Status = "done";
            await _moneyHistoryService.Update(mHistory);

            // Cập nhật số tiền trong profile
            Models.Profile pro = await _profileService.GetProfileByUsername(item);
            pro.Money += mHistory.Money;
            await _profileService.UpdateProfile(pro);

            // Optionally, store user information in cookies
            CookieOptions cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7), // Set cookie to expire in 7 days
                HttpOnly = true, // Prevents client-side JavaScript from accessing the cookie
                Secure = true // Ensures cookie is sent only over HTTPS
            };

            Response.Cookies.Append("Money", pro.Money.ToString(), cookieOptions);


            return Redirect("./Addmoney");
        }




    }
}
