using BussinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Models.ViewModel;

namespace WebApp.Pages.Information
{
    public class PurchaseHistoryModel : PageModel
    {
        [BindProperty]
        public IEnumerable<Purchase> OrderDetail { get; set; }
        public string Username { get; set; }
        private readonly IProfileService _profileService;

        public PurchaseHistoryModel(IProfileService profileService) // Inject IProfileService qua constructor
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
            Profile Profile = await _profileService.GetProfileByUsername(Username);

            OrderDetail = await _profileService.GetCountLibrary(Profile);
        }
    }
}
