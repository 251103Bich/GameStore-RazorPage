using BussinessLogic.Interfaces;
using BussinessLogic.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using System.ComponentModel.DataAnnotations;
using WebApp.ModelViews;

namespace WebApp.Pages.Home
{
    public class ConfirmCodeModel : PageModel
    {
        private readonly IProfileService _profileService;

        [BindProperty]
        public string code { get; set; }

        public ConfirmCodeModel(IProfileService profileService)
        {
            _profileService = profileService;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostConfirmCode()
        {
            var email = HttpContext.Session.GetString("SessionEmail");
            var type = HttpContext.Session.GetString("SessionType");
            var expert = HttpContext.Session.GetString("SessionExpiry");
            if (email != null && type != null && expert != null)
            {
                if (DateTime.UtcNow > DateTime.Parse(expert))
                {
                    return new JsonResult(new { message = "Session expired. Please Resend code." });
                }
                var temp = await _profileService.GetByEmail(email);
                if (temp != null) {
                    var check = await _profileService.CheckOTP(email, code, type);
                    if(check == true && type == "ResetPass")
                    {
                        HttpContext.Session.Clear();
                        HttpContext.Session.SetString("NameReset", temp.Username);
                        return new JsonResult(new { redirect = Url.Page("/Home/ResetPass") });
                    }
                    if (check == true)
                    {
                        HttpContext.Session.Clear();
                        TempData["SuccessMessage"] = "Your account confirm success!";
                        return new JsonResult(new { redirect = Url.Page("/Home/Success") });
                    }
                    else {
                        return new JsonResult(new { message = "OTP is inccorect!" });
                    }
                }
                

            }
            return new JsonResult(new { message = "Please login and confirm" });


        }

        public async Task<IActionResult> OnPostResend() {
            var email = HttpContext.Session.GetString("SessionEmail");
            var type = HttpContext.Session.GetString("SessionType");
            var expert = HttpContext.Session.GetString("SessionExpiry");

            if (email != null && type != null && expert != null)
            {
                var check = await _profileService.ResendOTP(email);
                if (check == true)
                {
                    var profile = await _profileService.GetByEmail(email);
                    SendEmail sendEmail = new SendEmail();
                    if(type == "Register")
                    {
                        await sendEmail.SendEmailAsync(profile.Email, "Confirm code your account", profile.Token);
                    }
                    if (type == "ResetPass")
                    {
                        await sendEmail.SendEmailAsync(profile.Email, "Confirm code your account", profile.Token);
                    }

                    HttpContext.Session.SetString("SessionEmail", email);
                    var expirationTime = DateTime.UtcNow.AddSeconds(60);
                    HttpContext.Session.SetString("SessionExpiry", expirationTime.ToString());
                    HttpContext.Session.SetString("SessionType", type);

                    return new JsonResult(new { message = "Send OTP to your mail!" });
                }
                else
                {
                    return new JsonResult(new { message = "Please login and confirm" });
                }

            }
            return new JsonResult(new { message = "Please login and confirm" });
        }
    }
    }
