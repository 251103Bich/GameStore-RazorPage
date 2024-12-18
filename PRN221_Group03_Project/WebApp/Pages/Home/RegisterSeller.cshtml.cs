using BussinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WebApp.ModelViews;

namespace WebApp.Pages.Home
{
    public class RegisterSellerModel : PageModel
    {
        private readonly IProfileService _profileService;
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }


        [BindProperty]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 15 characters.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]+$", ErrorMessage = "Username must contain both words and numbers. Dont have special word!")]
        public string username { get; set; }
        [BindProperty]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 15 characters.")]
        public string password { get; set; }
        [BindProperty]
        [Compare("password", ErrorMessage = "ConfirmPass do not match.")]
        public string confirmPass { get; set; }
        [BindProperty]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string email { get; set; }

        public RegisterSellerModel(IProfileService profileService) // Inject IProfileService qua constructor
        {
            _profileService = profileService;
        }
        public async Task<IActionResult> OnPostRegisterSeller()
        {
            var item = await _profileService.Register(username, password, email, "Seller");
            if (item == false)
            {
                ErrorMessage = "Email or Username is exist!";
                return Page();

            }
            var profile = await _profileService.GetProfileByUsername(username);
            SendEmail sendEmail = new SendEmail();
            await sendEmail.SendEmailAsync(profile.Email, "Confirm code your account", profile.Token);

            HttpContext.Session.SetString("SessionEmail", profile.Email);
            var expirationTime = DateTime.UtcNow.AddSeconds(60);
            HttpContext.Session.SetString("SessionExpiry", expirationTime.ToString());
            HttpContext.Session.SetString("SessionType", "Register");


            SuccessMessage = "Register Successfull || Pleaser into your mail and confirm";
            return RedirectToPage("/Home/ConfirmCode");

        }
    }
}
