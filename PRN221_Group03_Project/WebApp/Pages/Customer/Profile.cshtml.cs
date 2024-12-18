using BussinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Models;

namespace WebApp.Pages.Customer
{
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public Profile Profile { get; set; }

        [BindProperty]
        public int count { get; set; }
        [BindProperty]

        public string Username { get; set; }

        [BindProperty]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 15 characters.")]

        public string newPassword { get; set; }
        [Compare("newPassword", ErrorMessage = "Passwords do not match.")]

        public string confirmPassword { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage1 { get; set; }
        public string SuccessMessage1 { get; set; }

        private readonly IProfileService _profileService;
        public ProfileModel(IProfileService profileService) // Inject IProfileService qua constructor
        {
            _profileService = profileService;
        }


        public async Task OnGetAsync()
        {

            if (Request.Cookies.ContainsKey("Username"))
            {
                Username = Request.Cookies["Username"];
            }
            if (Username == null)
            {
                RedirectToPage("/Home/Index");
            }
            Profile = await _profileService.GetProfileByUsername(Username);
            var countTemp = await _profileService.GetCountLibrary(Profile);
            if (countTemp == null)
            {
                count = 0;
            }
            else
            {
                count = countTemp.Count();
            }

        }

        public async Task<IActionResult> OnPostChangePassword()
        {
            if (Request.Cookies.ContainsKey("Username"))
            {
                Username = Request.Cookies["Username"];
            }
            var check = await _profileService.ChangePassword(Username, newPassword);
            if (check == true)
            {
                SuccessMessage = "Change Password is Successfully!!";
                await OnGetAsync();
                return Page();
            }
            else
            {
                ErrorMessage = "Change Password is Failed!!";
                await OnGetAsync();
                return Page();
            }
        }
        public async Task<IActionResult> OnPostChangeInformation()
        {
            ModelState.Remove(nameof(newPassword));
            var check = _profileService.ChangeInformation(Profile.Username, Profile.Fullname, Profile.Gender, Profile.Birthday);
            if (check.Result == true)
            {
                SuccessMessage1 = "Change Information is Successfully!!";
                await OnGetAsync();

                return Page();
            }
            else
            {
                ErrorMessage1 = "Change Information is Failed!!";
                await OnGetAsync();

                return Page();

            }
        }
    }
}
