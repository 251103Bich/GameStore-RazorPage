using BussinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Models;
using WebApp.Hubs;
using WebApp.ViewModels;

namespace WebApp.Pages.Manage.Game
{
    public class IndexModel : PageModel
    {
        private readonly IGameService _gameService;
        private readonly ICategoryService _categoryService;
		private readonly IHubContext<SignalrHub> _hubContext;
        private readonly IProfileService _profileService;

        public IndexModel(IGameService gameService, ICategoryService categoryService, IHubContext<SignalrHub> hubContext, IProfileService profileService)
        {
            _gameService = gameService;
            _categoryService = categoryService;
			_hubContext = hubContext;
            _profileService = profileService;
        }

        [BindProperty]
        public IEnumerable<Models.Game> Games { get; set; }
        [BindProperty]
        public IEnumerable<Category> Categories { get; set; }
        [BindProperty]
        public Models.Game _Game { get; set; }
        [BindProperty]
        public Category _Category { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
			var cookie = Request.Cookies["Username"];
            var profile = await _profileService.GetProfileByUsername(cookie);
			Games = await _gameService.GetMyOwnGames(profile);
            Categories = await _categoryService.GetListAllCategory();
			return Page();
		}

        public async Task<IActionResult> OnGetDelete(string id)
        {
            var game = await _gameService.GetGameById(id);
            if (game != null)
            {
                await _gameService.DeleteGame(id);
				await _hubContext.Clients.All.SendAsync("loadData");
			}
            return RedirectToPage();
        }
    }
}
