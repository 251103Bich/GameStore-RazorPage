using BussinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Pages.Home
{
    public class ResetPassModel : PageModel
    {
        private readonly IProfileService _profileService;
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        [BindProperty]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 15 characters.")]
        public string newPass { get; set; }
        [BindProperty]
        [Compare("newPass", ErrorMessage = "ConfirmPass do not match.")]
        public string confirmPass { get; set; }

        public ResetPassModel(IProfileService profileService) // Inject IProfileService qua constructor
        {
            _profileService = profileService;
        }
        public async Task<IActionResult> OnGet()
        {
            var username = HttpContext.Session.GetString("NameReset");
            if (username == null) {
                return RedirectToPage("/Home/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var username = HttpContext.Session.GetString("NameReset");
            var check = await _profileService.ChangePassword(username, newPass);
            if (check == true)
            {
                HttpContext.Session.Clear();
                SuccessMessage = "ResetPass is Successfully!!";
                TempData["SuccessMessage"] = "ChangePassword Successfull!";
                return RedirectToPage("/Home/Success");
            }
            else
            {
                ErrorMessage = "ResetPass is Failed!!";
                return Page();
            }


        }
    }
}
