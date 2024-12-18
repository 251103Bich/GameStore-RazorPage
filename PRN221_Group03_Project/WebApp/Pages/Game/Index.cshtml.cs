using BussinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApp.Pages.Game
{
    public class IndexModel : PageModel
    {
        private readonly IGameService _gameService;
        private readonly ICategoryService _categoryService;
        private readonly IProfileService _profileService;

        public IndexModel(IGameService gameService, ICategoryService categoryService, IProfileService profileService)
        {
            _gameService = gameService;
            _categoryService = categoryService;
            _profileService = profileService;
        }

        [BindProperty]
        public Models.Game viewedGame { get; set; }
        [BindProperty]
        public Models.Feedback NewFeedback { get; set; }
        [BindProperty]
        public IEnumerable<Models.Feedback> Feedbacks { get; set; }
        [BindProperty]
        public IEnumerable<Category> categories { get; set; }
        [BindProperty]
        public bool checkCart { get; set; } = false;
        public bool checkBuy { get; set; }
        public bool checkWishList { get; set; } = false;
        public bool checkLibrary { get; set; } = false;
        public bool checkFeedback { get; set; } = false;

        public async Task OnGetAsync(string id)
        {
            var user = Request.Cookies["Username"];

            if (user != null)
            {
                checkCart = await _profileService.GetCheckLibrary(user, id);
            }

            viewedGame = await _gameService.GetGameById(id);
            categories = await _categoryService.GetCategoriesByGameId(id);
            Feedbacks = await _gameService.GetAllFeedbackByGID(id);
            NewFeedback = new Models.Feedback
            {
                Gid = id
            };

            var profile = await _profileService.GetProfileByUsername(user);
            if (profile != null)
            {
                checkWishList = profile.GidsNavigation.Any(g => g.Gid == id);


                var library = await _profileService.GetUserLibraryAsync(await _profileService.GetProfileByUsername(user));

                if (library.Any(g => g.Gid == viewedGame.Gid))
                {
                    checkLibrary = true;
                    if (Feedbacks.Any(u => u.Username == user))
                    {
                        checkFeedback = true;
                    }
                }
            }
        }
        public async Task<RedirectToPageResult> OnPostAsync(string id)
        {
            var user = Request.Cookies["Username"];
            if (user == null)
            {
                return RedirectToPage("/Home/Login");
            }
            checkCart = await _gameService.CheckCart(user, id);
            if (!checkCart)
            {
                await _gameService.AddCard(id, user);
            }
            return RedirectToPage("/ShoppingCard/Index");
        }
        public async Task<IActionResult> OnPostSubmitReview()
        {
            var cookie = Request.Cookies["Username"];
            if (cookie == null)
            {
                return RedirectToPage("/Home/Login");
            }
            await _profileService.AddFeedbackAsync(NewFeedback, cookie);
            return RedirectToPage(new { id = NewFeedback.Gid });
        }

		public async Task<IActionResult> OnPostAddWishlist(string gid)
		{
            var cookie = Request.Cookies["Username"];
            if (cookie == null)
            {
                return RedirectToPage("/Home/Login");
            }
            var profile = await _profileService.GetProfileByUsername(cookie);
			await _profileService.UpdateWishlist(profile, gid);
			return RedirectToPage(new { id = NewFeedback.Gid });
            
		}
	}
}
