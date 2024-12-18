using BussinessLogic.Interfaces;
using BussinessLogic.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using WebApp.Hubs;

namespace WebApp.Pages.Wishlist
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
        public Models.Profile _Profile { get; set; }

		public async Task<IActionResult> OnGetAsync()
        {
            var cookie = Request.Cookies["Username"];
            _Profile = await _profileService.GetProfileByUsername(cookie);
			return Page();

		}

        public async Task<IActionResult> OnPostAsync(string gid)
        {
			var cookie = Request.Cookies["Username"];
			var profile = await _profileService.GetProfileByUsername(cookie);
			await _profileService.UpdateWishlist(profile, gid);
			await OnGetAsync();
			return Page();
		}

		public async Task<IActionResult> OnPostAddCartAsync(string gid)
		{
			var cookie = Request.Cookies["Username"];
			var profile = await _profileService.GetProfileByUsername(cookie);
			await _gameService.AddCard(gid, cookie);
			await OnGetAsync();
			return Page();
		}
	}
}
