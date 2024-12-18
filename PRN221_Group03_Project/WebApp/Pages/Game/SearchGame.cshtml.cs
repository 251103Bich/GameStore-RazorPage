using BussinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Game
{
    public class SearchGameModel : PageModel
    {
        private readonly IGameService _gameService;

        public SearchGameModel(IGameService gameService)
        {
            _gameService = gameService;
        }

        public IEnumerable<Models.Game> Games { get; set; }
        public async Task OnGetAsync()
        {
            Games = await _gameService.GetListAllGame();
        }
        public async Task<IActionResult> OnGetGamePartialAsync(string searchTerm)
        {

            if (string.IsNullOrEmpty(searchTerm))
            {
                Games = await _gameService.GetListAllGame();
            }
            else
            {
                Games = await _gameService.GetGamesByGameName(searchTerm);
            }

            return new PartialViewResult
            {
                ViewName = "_GameList",
                ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IEnumerable<Models.Game>>(
            ViewData, Games)
            };
        }
    }
}
