using BussinessLogic.Interfaces;
using Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Service
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _repo;

		public CategoryService(ICategoryRepository item)
		{
			_repo = item;
		}

		public async Task<IEnumerable<Category>> GetListAllCategory()
		{
			return await _repo.GetListAll();
		}

		public async Task<Category> GetCategoryById(string id)
		{
			return await _repo.GetById(id);
		}

		public async Task<IEnumerable<Category>> GetCategoriesByGameId(string id)
		{
			return await _repo.GetCategoriesByGameId(id);
		}


    }
}
