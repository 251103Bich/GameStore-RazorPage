using BussinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace WebApp.Pages.Administrator.Revenue
{
    public class IndexModel : PageModel
    {
        private readonly IMoneyManagementSevice _moneyManagementSevice;
        private readonly IProfileService _profileService;

        public IndexModel(IMoneyManagementSevice moneyManagementSevice, IProfileService profileService)
        {
            _moneyManagementSevice = moneyManagementSevice;
            _profileService = profileService;
        }

        public IEnumerable<(string ID, string GID, DateTime date, long sellerMoney, long plusMoney)> totalRevenue {  get; set; }
        public IEnumerable<Profile> sellerList { get; set; }
        public List<DateOnly> dateNames { get; set; }
        public List<long> dateRevenue { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var admin = Request.Cookies["AdminName"];

            if (admin == null)
            {
                return RedirectToPage("/Home/Index");
            }

            totalRevenue = await _moneyManagementSevice.GetWebTotalRevene();
            sellerList = await _profileService.GetUserByType("Seller");

            dateNames = totalRevenue
                .Select(x => DateOnly.FromDateTime(x.date))
                .Distinct()
                .ToList();

            dateRevenue = dateNames
                .Select(date => totalRevenue
                    .Where(x => DateOnly.FromDateTime(x.date) == date)
                    .Sum(x => x.plusMoney))
                .ToList();

            ViewData["DateNames"] = dateNames;
            ViewData["DateRevenue"] = dateRevenue;

            return Page();
        }
    }
}
