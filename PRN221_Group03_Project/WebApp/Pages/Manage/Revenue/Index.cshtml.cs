using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Interfaces;

namespace WebApp.Pages.Manage.Revenue
{
    public class IndexModel : PageModel
    {
        private readonly IMoneyManagementRepository _moneyManagementRepository;
        private readonly IGameRepository _gameRepository;

        public IndexModel(IMoneyManagementRepository moneyManagementRepository, IGameRepository gameRepository)
        {
            _moneyManagementRepository = moneyManagementRepository;
            _gameRepository = gameRepository;
        }

        public IEnumerable<(DateTime date, long revenue)> timeRevenue {  get; set; }
        public IEnumerable<Models.Game> sellerGames { get; set; }

        public List<DateOnly> dateNames { get; set; }
        public List<long> dateRevenue { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var seller = Request.Cookies["SellerName"];

            if (seller == null)
            {
                return RedirectToPage("/Home/Index");
            }

            timeRevenue = await _moneyManagementRepository.GetTimeRevenue(seller);
            sellerGames = await _gameRepository.GetGamesBySellerName(seller);

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
