using BussinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Models;
using WebApp.Hubs;

namespace WebApp.Pages.Manage.Game
{
    public class AddNewModel : PageModel
    {
        private readonly IGameService _gameService;
        private readonly ICategoryService _categoryService;
		private readonly IHubContext<SignalrHub> _hubContext;

		public AddNewModel(IGameService gameService, ICategoryService categoryService, IHubContext<SignalrHub> hubContext)
        {
            _gameService = gameService;
            _categoryService = categoryService;
			_hubContext = hubContext;
		}

        [BindProperty]
        public IEnumerable<Category> Categories { get; set; }
        [BindProperty]
        public Models.Game _Game { get; set; }
        [BindProperty]
        public string cidList { get; set; }
		[BindProperty]
		public IFormFile Image { get; set; }
        [BindProperty]
        public IFormFile FileDownload { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var cookie = Request.Cookies["Username"];
            Categories = await _categoryService.GetListAllCategory();
			return Page();
		}

        public async Task<IActionResult> OnPostCreate()
        {
            if (Image != null && Image.Length > 0 && FileDownload != null && FileDownload.Length > 0 )
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload/img", Image.FileName);
                _Game.Picture = Image.FileName;
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }

                var filePathDownload = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload/file", FileDownload.FileName);
                _Game.DownloadFile = FileDownload.FileName;
                using (var stream = new FileStream(filePathDownload, FileMode.Create))
                {
                    await FileDownload.CopyToAsync(stream);
                }
            }
            _Game.SellerName = Request.Cookies["Username"];
            await _gameService.AddGame(_Game, cidList.Split(",").ToList());
			await _hubContext.Clients.All.SendAsync("loadData");
			return RedirectToPage("./Index");
        }
    }
}
