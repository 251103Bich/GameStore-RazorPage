using BussinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WebApp.ModelViews;

namespace WebApp.Pages.Home
{
    public class ForgetPasswordModel : PageModel
    {
        private readonly IProfileService _profileService;
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        [BindProperty]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string email { get; set; }

        public ForgetPasswordModel(IProfileService profileService) // Inject IProfileService qua constructor
        {
            _profileService = profileService;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            var profile = await _profileService.GetByEmail(email);
            if (profile == null)
            {
                ErrorMessage = "Not found account reference your email!";
                return Page();
            }
            var check = await _profileService.ResendOTP(profile.Email);
            if (check == true)
            {
                SendEmail sendEmail = new SendEmail();
                await sendEmail.SendEmailAsync(profile.Email, "Confirm code ResetPassword", profile.Token);

                HttpContext.Session.SetString("SessionEmail", profile.Email);
                var expirationTime = DateTime.UtcNow.AddSeconds(60);
                HttpContext.Session.SetString("SessionExpiry", expirationTime.ToString());
                HttpContext.Session.SetString("SessionType", "ResetPass");

                return RedirectToPage("/Home/ConfirmCode");
            }
            else {
                ErrorMessage = "Send Mail Error!!!";
                return Page();
            }
        }
    }
}
