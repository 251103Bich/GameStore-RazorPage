using BussinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace WebApp.Pages.Customer
{
    public class FundsHistoryModel : PageModel
    {
        private readonly IMoneyHistoryService _moneyHistoryService;
        public FundsHistoryModel(IMoneyHistoryService moneyHistoryService)
        {
            _moneyHistoryService = moneyHistoryService;
        }
        public IEnumerable<MoneyHistory> moneyHistories { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = Request.Cookies["Username"];
            if (user == null)
            {
                return RedirectToPage("/Home/Index");
            }
            moneyHistories = await _moneyHistoryService.GetByUsername(user);

            return Page();
        }
    }
}
