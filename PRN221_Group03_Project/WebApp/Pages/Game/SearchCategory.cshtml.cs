using BussinessLogic.Interfaces;
using BussinessLogic.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace WebApp.Pages.Game
{
    public class SearchCategoryModel : PageModel
    {
        private readonly IGameService gameService;
        private readonly ICategoryService categoryService;
        public Category cate;
        public SearchCategoryModel(IGameService gameService, ICategoryService categoryService)
        {
            this.gameService = gameService;
            this.categoryService = categoryService;
        }
        public IEnumerable<Models.Game> listGame;
        public async Task OnGetAsync(string id)
        {
            var CategoryId = id;
            cate = await categoryService.GetCategoryById(CategoryId);
            listGame = await gameService.searchGameByCategory(CategoryId);
        }
    }
}
