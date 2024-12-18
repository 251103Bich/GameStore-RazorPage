using BussinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using WebApp.Hubs;

namespace WebApp.Pages.Library
{
	public class IndexModel : PageModel
	{
		private readonly IProfileService _profileService;
		private readonly IGameService _gameService;
		private readonly IHubContext<SignalrHub> _hubContext;

		public IndexModel(IProfileService profileService, IGameService gameService, IHubContext<SignalrHub> hubContext)
		{
			_profileService = profileService;
			_gameService = gameService;
			_hubContext = hubContext;
		}

		[BindProperty]
		public IEnumerable<Models.Game> Games { get; set; }
		[BindProperty]
		public Models.Profile Profile { get; set; }

		public async Task<IActionResult> OnGetAsync()
		{
			var cookie = Request.Cookies["Username"];
			var profile = await _profileService.GetProfileByUsername(cookie);
			Profile = profile;
			Games = await _profileService.GetUserLibraryAsync(profile);
			return Page();
		}
	}
}
