using BussinessLogic.Interfaces;
using BussinessLogic.Service;
using DataAccess.DAOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using System.Collections.Generic;
using WebApp.ModelViews;

namespace WebApp.Pages.Game
{
    public class CategoryListModel : PageModel
    {
        private readonly IGameService _IGameService;
        private readonly ICategoryService _ICategoryService;

        [BindProperty]
        public IEnumerable<Models.Game> GamesList { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }
        public CategoryListModel(IGameService igameService, ICategoryService iCategoryService)
        {
            _IGameService = igameService;
            _ICategoryService = iCategoryService;
        }
        public async Task OnGetAsync()
        {

            GamesList = await _IGameService.GetListAllGame();
            CategoryList = await _ICategoryService.GetListAllCategory();

        }

    }
}
