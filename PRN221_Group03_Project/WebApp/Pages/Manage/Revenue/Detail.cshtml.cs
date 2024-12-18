using BussinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Manage.Revenue
{
    public class DetailModel : PageModel
    {
        private readonly IMoneyManagementSevice _moneyManagementSevice;
        private readonly IGameService _gameService;

        public DetailModel(IMoneyManagementSevice moneyManagementSevice, IGameService gameService)
        {
            _moneyManagementSevice = moneyManagementSevice;
            _gameService = gameService;
        }


        public Models.Game game { get; set; }
        public IEnumerable<(string ID, DateTime date, string cusName, long gamePrice, long sellerPlusMoney)> detailGameRevenue {  get; set; }
        public List<DateOnly> dateNames { get; set; }
        public List<long> dateRevenue { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var seller = Request.Cookies["SellerName"];

            if (seller == null)
            {
                return RedirectToPage("/Home/Index");
            }

            detailGameRevenue = await _moneyManagementSevice.GetGameRevenue(seller, id);
            game = await _gameService.GetGameById(id);

            dateNames = detailGameRevenue
                .Select(x => DateOnly.FromDateTime(x.date))
                .Distinct()
                .ToList();

            dateRevenue = dateNames
                .Select(date => detailGameRevenue
                    .Where(x => DateOnly.FromDateTime(x.date) == date)
                    .Sum(x => x.sellerPlusMoney))
                .ToList();

            ViewData["DateNames"] = dateNames;
            ViewData["DateRevenue"] = dateRevenue;

            return Page();
        }
    }
}
