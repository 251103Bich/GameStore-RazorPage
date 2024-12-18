using BussinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Models;
using WebApp.Hubs;

namespace WebApp.Pages.Manage.Game
{
    public class EditModel : PageModel
    {
        private readonly IGameService _gameService;
        private readonly ICategoryService _categoryService;
        private readonly IHubContext<SignalrHub> _hubContext;

        public EditModel(IGameService gameService, ICategoryService categoryService, IHubContext<SignalrHub> hubContext)
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
        public List<Category> checker { get; set; }
        [BindProperty]
        public IFormFile Image { get; set; }
        [BindProperty]
        public IFormFile FileDownload { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var cookie = Request.Cookies["Username"];
            _Game = await _gameService.GetGameById(id);
            Categories = await _categoryService.GetListAllCategory();
            checker = await _gameService.GetCategoryByGameId(id);
			return Page();
		}

        public async Task<IActionResult> OnPostEditAsync()
        {
            var game = await _gameService.GetGameById(_Game.Gid);
            if (Image != null && Image.Length > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload/img", Image.FileName);
                _Game.Picture = Image.FileName;
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }
            }
            else
            {
                _Game.Picture = game.Picture;
            }

            if (FileDownload != null && FileDownload.Length > 0)
            {
                var filePathDownload = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload/file", FileDownload.FileName);
                _Game.DownloadFile = FileDownload.FileName;
                using (var stream = new FileStream(filePathDownload, FileMode.Create))
                {
                    await FileDownload.CopyToAsync(stream);
                }
            }
            else
            {
                _Game.DownloadFile = game.DownloadFile;
            }
            await _gameService.UpdateGame(_Game, cidList.Split(",").ToList());
            await _hubContext.Clients.All.SendAsync("loadData");
            return RedirectToPage("./Index");
        }
    }
}
