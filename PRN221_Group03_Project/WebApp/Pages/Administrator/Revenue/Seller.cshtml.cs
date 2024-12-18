using BussinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Administrator.Revenue
{
    public class SellerModel : PageModel
    {
        private readonly IMoneyManagementSevice _moneyManagementService;
        private readonly IGameService _gameService;
        private readonly IProfileService _profileService;

        public SellerModel(IMoneyManagementSevice moneyManagementService, IGameService gameService, IProfileService profileService)
        {
            _moneyManagementService = moneyManagementService;
            _gameService = gameService;
            _profileService = profileService;
        }

        public IEnumerable<(DateTime date, long revenue)> timeRevenue { get; set; }
        public IEnumerable<Models.Game> sellerGames { get; set; }

        public List<DateOnly> dateNames { get; set; }
        public List<long> dateRevenue { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var admin = Request.Cookies["AdminName"];

            if (admin == null)
            {
                return RedirectToPage("/Home/Index");
            }



            timeRevenue = await _moneyManagementService.GetTimeRevenue(id);
            sellerGames = await _gameService.GetGamesBySellerName(id);

            dateNames = timeRevenue
                .Select(x => DateOnly.FromDateTime(x.date))
                .Distinct()
                .ToList();

            dateRevenue = dateNames
                .Select(date => timeRevenue
                    .Where(x => DateOnly.FromDateTime(x.date) == date)
                    .Sum(x => x.revenue))
                .ToList();

            ViewData["DateNames"] = dateNames;
            ViewData["DateRevenue"] = dateRevenue;

            return Page();
        }
    }
}
