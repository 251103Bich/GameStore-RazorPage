using BussinessLogic.Interfaces;
using BussinessLogic.Service;
using DataAccess;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using WebApp.ModelViews;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Pages.Home
{
    public class LoginModel : PageModel
    {
        private readonly IProfileService _profileService;
        [BindProperty]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 15 characters.")]

        public string username { get; set; }
        [BindProperty]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 15 characters.")]
        public string password { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public LoginModel(IProfileService profileService) // Inject IProfileService qua constructor
        {
            _profileService = profileService;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost() { 
           var temp = await _profileService.Login(username, password);
            if (temp == null)
            {
                ErrorMessage = "Username or Password is Wrong!";
                return Page();
            }
            if (temp.Status == "Confirm")
            {
                var check = await _profileService.ResendOTP(temp.Email);
                if (check == true)
                {
                    var profile = await _profileService.GetByEmail(temp.Email);
                    SendEmail sendEmail = new SendEmail();
                    await sendEmail.SendEmailAsync(profile.Email, "Confirm code your account", profile.Token);

                    HttpContext.Session.SetString("SessionEmail", temp.Email);
                    var expirationTime = DateTime.UtcNow.AddSeconds(60);
                    HttpContext.Session.SetString("SessionExpiry", expirationTime.ToString());
                    HttpContext.Session.SetString("SessionType", "Register");

                    return RedirectToPage("/Home/ConfirmCode");
                }
            }
            if (temp.Status == "Disable")
            {
                ErrorMessage = "Your Account is Disable!";
                return Page();
            }
            if (temp.Status == "Waiting")
            {
                ErrorMessage = "Your Account is Waiting Accept!";
                return Page();
            }
            ErrorMessage = temp.Username;
            // If login succeeds, store user information in the session
            HttpContext.Session.SetString("Username", temp.Username);
            


            // Optionally, store user information in cookies
            CookieOptions cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7), // Set cookie to expire in 7 days
                HttpOnly = true, // Prevents client-side JavaScript from accessing the cookie
                Secure = true // Ensures cookie is sent only over HTTPS
            };

            // Store the username in a cookie
            Response.Cookies.Append("Username", temp.Username, cookieOptions);
            Response.Cookies.Append("Money", temp.Money.ToString(), cookieOptions);

            if (temp.Type == "Seller" && temp.Status == "Enable") {
                Response.Cookies.Append("SellerName", temp.Username, cookieOptions);
            }
            if (temp.Type == "Admin" && temp.Status == "Enable")
            {
                Response.Cookies.Append("AdminName", temp.Username, cookieOptions);
            }
            return RedirectToPage("/Home/Index");

        }
        public async Task OnPostLoginWithGoogle()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                 RedirectUri = Url.Page("Login", "GoogleRespone")// Points to GoogleRespone handler
            });
        }
        public async Task<IActionResult> OnGetGoogleResponeAsync()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claim = result.Principal.Identities.FirstOrDefault().Claims;

            // Tìm `claim` có `Type` là email
            string email = claim.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            CookieOptions cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7), // Set cookie to expire in 7 days
                HttpOnly = true, // Prevents client-side JavaScript from accessing the cookie
                Secure = true // Ensures cookie is sent only over HTTPS
            };

            
            var temp = await _profileService.GetByEmail(email);

            if (temp == null)
            { 
                var check = await _profileService.RegisterGoogle(email, email, email, "Customer");
                var profile = await _profileService.GetByEmail(email);
                Response.Cookies.Append("Username", profile.Username, cookieOptions);
                Response.Cookies.Append("Money", profile.Money.ToString(), cookieOptions);
                return RedirectToPage("/Home/Index");
            }
            else {
                if (temp.Status == "Confirm")
                {
                    var confirm = await _profileService.ConfirmGoogle(email);
                }
                var profile = await _profileService.GetByEmail(email);
                if(profile.Status == "Waiting")
                {
                    SuccessMessage = "Your Account Confirm Success, Please Waiting Admin Accept!";
                    return Page();
                }
                if (temp.Status == "Disable")
                {
                    ErrorMessage = "Your Account is Disable!";
                    return Page();
                }
                Response.Cookies.Append("Username", temp.Username, cookieOptions);
                Response.Cookies.Append("Money", temp.Money.ToString(), cookieOptions);
                if (temp.Type == "Seller" && temp.Status == "Enable")
                {
                    Response.Cookies.Append("SellerName", temp.Username, cookieOptions);
                }
                if (temp.Type == "Admin" && temp.Status == "Enable")
                {
                    Response.Cookies.Append("AdminName", temp.Username, cookieOptions);
                }
                return RedirectToPage("/Home/Index");
            }

        }

    }
}
