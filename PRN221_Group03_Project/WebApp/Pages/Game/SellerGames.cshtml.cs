using BussinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Game
{
    public class SellerGamesModel : PageModel
    {
        private readonly IGameService _gameService;

        public SellerGamesModel(IGameService gameService)
        {
            _gameService = gameService;
        }

        [BindProperty]
        public IEnumerable<Models.Game> sellerGames { get; set; }

        public async Task OnGetAsync(string seller)
        {
            sellerGames = await _gameService.GetGamesBySellerName(seller);
        }
    }
}
