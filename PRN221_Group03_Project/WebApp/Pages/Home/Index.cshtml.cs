using BussinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Home
{
    public class IndexModel : PageModel
    {
        private readonly IGameService _gameService;

        public IndexModel(IGameService service)
        {
            _gameService = service;
        }

        [BindProperty]
        public IEnumerable<Models.Game> Games { get; set; }
        [BindProperty]
        public IEnumerable<Models.Game> TopNewGames { get; set; }
        [BindProperty]
        public IEnumerable<Models.Game> TopSaleGames { get; set; }

        public async Task OnGetAsync()
        {
            Games = await _gameService.GetListAllGame();
            TopNewGames = await _gameService.GetNewGameList();
            TopSaleGames = await _gameService.GetTopSaleGameList();
        }


    }
}
